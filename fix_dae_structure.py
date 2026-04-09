#!/usr/bin/env python3
import sys
import xml.etree.ElementTree as ET

dae_file = "unpacked/jurassic_beam_park_localfix.disabled/levels/jurassic_beam_park/art/shapes/JP-cage/jp-raptorcagecomplete.dae"
IDENTITY_MATRIX = "1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1"

# ─────────────────────────────────────────────────────────────────────────────
# VERIFY MODE
# Run:  python fix_dae_structure.py --verify
# Checks every critical requirement for BeamNG / Torque3D COLLADA import.
# ─────────────────────────────────────────────────────────────────────────────

PASS = "[ PASS ]"
FAIL = "[ FAIL ]"
WARN = "[ WARN ]"
INFO = "[ INFO ]"


def _row(status: str, label: str, detail: str = "") -> str:
    detail_str = f"  →  {detail}" if detail else ""
    return f"  {status}  {label}{detail_str}"


def verify(path: str) -> int:
    SEP = "─" * 68
    print()
    print("  ╔══════════════════════════════════════════════════════════════════╗")
    print("  ║          BeamNG DAE Import Verification Console                 ║")
    print("  ╚══════════════════════════════════════════════════════════════════╝")
    print(f"  File : {path}")
    print(f"  {SEP}")

    results: list[tuple[bool | None, str]] = []  # (passed, row_string)

    # ── 1. XML well-formed ────────────────────────────────────────────────────
    try:
        tree = ET.parse(path)
        root = tree.getroot()
        results.append((True, _row(PASS, "XML well-formed")))
    except ET.ParseError as exc:
        results.append((False, _row(FAIL, "XML well-formed", str(exc))))
        _print_results(results)
        return 1
    except FileNotFoundError:
        results.append((False, _row(FAIL, "File exists", path)))
        _print_results(results)
        return 1

    ns = get_namespace(root.tag)

    # ── 2. COLLADA root element ───────────────────────────────────────────────
    local_root = root.tag.split("}")[-1] if "}" in root.tag else root.tag
    if local_root == "COLLADA":
        results.append((True, _row(PASS, "Root element is <COLLADA>")))
    else:
        results.append((False, _row(FAIL, "Root element is <COLLADA>", f"got <{local_root}>")))

    # ── 3. library_geometries present and non-empty ───────────────────────────
    lib_geom = root.find(f".//{qname(ns, 'library_geometries')}")
    geom_list = lib_geom.findall(qname(ns, "geometry")) if lib_geom is not None else []
    if geom_list:
        results.append((True, _row(PASS, "library_geometries present", f"{len(geom_list)} geometry block(s)")))
    else:
        results.append((False, _row(FAIL, "library_geometries present", "none found — nothing to import")))

    # ── 4. Each geometry has <mesh> with <triangles> or <polylist> ────────────
    bad_geoms = []
    for g in geom_list:
        gid = g.get("id", "?")
        mesh = g.find(qname(ns, "mesh"))
        if mesh is None:
            bad_geoms.append(f"{gid}:no<mesh>")
            continue
        has_tris = mesh.find(qname(ns, "triangles")) is not None
        has_poly = mesh.find(qname(ns, "polylist")) is not None
        if not (has_tris or has_poly):
            bad_geoms.append(f"{gid}:no<triangles>/<polylist>")
    if not bad_geoms:
        results.append((True, _row(PASS, "All geometries have triangles/polylist")))
    else:
        results.append((False, _row(FAIL, "All geometries have triangles/polylist", "; ".join(bad_geoms))))

    # ── 5. library_effects + library_materials present ────────────────────────
    has_effects = root.find(f".//{qname(ns, 'library_effects')}") is not None
    has_mats = root.find(f".//{qname(ns, 'library_materials')}") is not None
    if has_effects and has_mats:
        results.append((True, _row(PASS, "library_effects and library_materials present")))
    else:
        missing = []
        if not has_effects:
            missing.append("library_effects")
        if not has_mats:
            missing.append("library_materials")
        results.append((False, _row(FAIL, "library_effects and library_materials present", f"missing: {', '.join(missing)}")))

    # ── 6. visual_scene id="Scene" exists ─────────────────────────────────────
    visual_scene = root.find(f".//{qname(ns, 'visual_scene')}[@id='Scene']")
    if visual_scene is not None:
        results.append((True, _row(PASS, "visual_scene id='Scene' found")))
    else:
        results.append((False, _row(FAIL, "visual_scene id='Scene' found", "BeamNG won't load without it")))
        _print_results(results)
        return 1

    node_tag = qname(ns, "node")

    # ── 7. base00 hierarchy ───────────────────────────────────────────────────
    base_node = find_child_by_id(visual_scene, ns, "base00")
    if base_node is not None:
        results.append((True, _row(PASS, "base00 node present under visual_scene")))
    else:
        results.append((False, _row(FAIL, "base00 node present under visual_scene", "run tool without --verify to fix")))

    # ── 8. start01 under base00 ───────────────────────────────────────────────
    start_node = find_child_by_id(base_node, ns, "start01") if base_node is not None else None
    if start_node is not None:
        results.append((True, _row(PASS, "start01 node present under base00")))
    else:
        results.append((False, _row(FAIL, "start01 node present under base00", "run tool without --verify to fix")))

    # ── 9. Colmesh nodes under start01 (sibling to visual nodes) ─────────────
    if start_node is not None:
        col_nodes_at_start = [
            c for c in list(start_node)
            if c.tag == node_tag and c.get("id", "").startswith("Colmesh-")
        ]
    else:
        col_nodes_at_start = []
    if col_nodes_at_start:
        results.append((True, _row(PASS, "Colmesh nodes under start01 (sibling level)")))
    else:
        results.append((False, _row(FAIL, "Colmesh nodes under start01 (sibling level)", "run tool without --verify to fix")))

    # ── 10. SM_Deco visual nodes exist under start01 ─────────────────────────
    vis_nodes = []
    if start_node is not None:
        for child in list(start_node):
            if child.tag == node_tag and child.get("id", "").startswith("SM_Deco"):
                vis_nodes.append(child)
    if vis_nodes:
        ids = ", ".join(n.get("id", "?") for n in vis_nodes)
        results.append((True, _row(PASS, f"SM_Deco visual nodes present ({len(vis_nodes)})", ids)))
    else:
        results.append((False, _row(FAIL, "SM_Deco visual nodes present", "no renderable mesh nodes found")))

    # ── 11. Each SM_Deco node has bind_material + instance_material ───────────
    bad_vis = []
    for vn in vis_nodes:
        inst = vn.find(qname(ns, "instance_geometry"))
        if inst is None:
            bad_vis.append(f"{vn.get('id','?')}:no instance_geometry")
            continue
        bm = inst.find(qname(ns, "bind_material"))
        if bm is None:
            bad_vis.append(f"{vn.get('id','?')}:missing bind_material")
            continue
        tc = bm.find(qname(ns, "technique_common"))
        im = tc.find(qname(ns, "instance_material")) if tc is not None else None
        if im is None:
            bad_vis.append(f"{vn.get('id','?')}:missing instance_material")
            continue
        bvi = im.find(qname(ns, "bind_vertex_input"))
        if bvi is None:
            bad_vis.append(f"{vn.get('id','?')}:missing bind_vertex_input (UV set)")
    if not bad_vis:
        results.append((True, _row(PASS, "All visual nodes have bind_material + UV binding")))
    else:
        results.append((False, _row(FAIL, "All visual nodes have bind_material + UV binding", "; ".join(bad_vis))))

    # ── 12. Each SM_Deco bind_material target resolves to a known material ────
    mat_ids = set()
    lib_mats = root.find(f".//{qname(ns, 'library_materials')}")
    if lib_mats is not None:
        for m in lib_mats.findall(qname(ns, "material")):
            mat_ids.add(f"#{m.get('id','')}")
    unresolved = []
    for vn in vis_nodes:
        inst = vn.find(qname(ns, "instance_geometry"))
        if inst is None:
            continue
        bm = inst.find(qname(ns, "bind_material"))
        if bm is None:
            continue
        tc = bm.find(qname(ns, "technique_common"))
        if tc is None:
            continue
        for im in tc.findall(qname(ns, "instance_material")):
            target = im.get("target", "")
            if target and target not in mat_ids:
                unresolved.append(f"{vn.get('id','?')}→{target}")
    if not unresolved:
        results.append((True, _row(PASS, "All material targets resolve in library_materials")))
    else:
        results.append((False, _row(FAIL, "All material targets resolve", "; ".join(unresolved))))

    # ── 13. Colmesh nodes present under start01 ─────────────────────────────
    col_nodes = []
    if start_node is not None:
        for child in list(start_node):
            if child.tag == node_tag and child.get("id", "").startswith("Colmesh-"):
                col_nodes.append(child)
    if col_nodes:
        ids = ", ".join(n.get("id", "?") for n in col_nodes)
        results.append((True, _row(PASS, f"Colmesh nodes present under start01 ({len(col_nodes)})", ids)))
    else:
        results.append((False, _row(FAIL, "Colmesh nodes present under start01", "no collision geometry — run tool without --verify")))

    # ── 14. Colmesh geometry URLs resolve to known geometry IDs ──────────────
    geom_ids = {f"#{g.get('id','')}" for g in geom_list}
    bad_col = []
    for cn in col_nodes:
        inst = cn.find(qname(ns, "instance_geometry"))
        if inst is None:
            bad_col.append(f"{cn.get('id','?')}:no instance_geometry")
            continue
        url = inst.get("url", "")
        if url not in geom_ids:
            bad_col.append(f"{cn.get('id','?')}→{url} (not found)")
    if not bad_col:
        results.append((True, _row(PASS, "All Colmesh geometry URLs resolve")))
    else:
        results.append((False, _row(FAIL, "All Colmesh geometry URLs resolve", "; ".join(bad_col))))

    # ── 15. Colmesh nodes have NO bind_material (collision nodes must be unbound)
    col_with_mat = []
    for cn in col_nodes:
        inst = cn.find(qname(ns, "instance_geometry"))
        if inst is not None and inst.find(qname(ns, "bind_material")) is not None:
            col_with_mat.append(cn.get("id", "?"))
    if not col_with_mat:
        results.append((True, _row(PASS, "Colmesh nodes have no bind_material (correct)")))
    else:
        results.append((None, _row(WARN, "Colmesh nodes have bind_material", f"{col_with_mat} — may be ignored but not ideal")))

    # ── 16. No stray nodes at visual_scene root (everything wrapped in base00) ─
    stray = [
        c.get("id", "?")
        for c in list(visual_scene)
        if c.tag == node_tag and c.get("id") != "base00"
    ]
    if not stray:
        results.append((True, _row(PASS, "No stray nodes at visual_scene root")))
    else:
        results.append((False, _row(FAIL, "No stray nodes at visual_scene root", f"found: {stray}")))

    # ── 17. instance_visual_scene points to Scene ────────────────────────────
    ivs = root.find(f".//{qname(ns, 'instance_visual_scene')}")
    if ivs is not None and ivs.get("url") == "#Scene":
        results.append((True, _row(PASS, "instance_visual_scene url='#Scene'")))
    else:
        got = ivs.get("url", "missing") if ivs is not None else "no instance_visual_scene element"
        results.append((False, _row(FAIL, "instance_visual_scene url='#Scene'", f"got: {got}")))

    _print_results(results)
    return 0


def _print_results(results: list) -> None:
    SEP = "─" * 68
    passed = sum(1 for ok, _ in results if ok is True)
    failed = sum(1 for ok, _ in results if ok is False)
    warned = sum(1 for ok, _ in results if ok is None)
    total = len(results)

    print()
    for _, row in results:
        print(row)
    print()
    print(f"  {SEP}")
    if failed == 0:
        verdict = "ALL CHECKS PASSED — file is BeamNG import ready"
    else:
        verdict = f"{failed} CHECK(S) FAILED — fix before importing"
    print(f"  {verdict}")
    print(f"  Passed: {passed}/{total}   Failed: {failed}   Warnings: {warned}")
    print(f"  {SEP}")
    print()


def get_namespace(tag: str) -> str:
    if tag.startswith("{") and "}" in tag:
        return tag[1 : tag.index("}")]
    return ""


def qname(ns: str, local: str) -> str:
    return f"{{{ns}}}{local}" if ns else local


def find_child_by_id(parent: ET.Element, ns: str, node_id: str) -> ET.Element | None:
    node_tag = qname(ns, "node")
    for child in list(parent):
        if child.tag == node_tag and child.get("id") == node_id:
            return child
    return None


def ensure_identity_matrix(node: ET.Element, ns: str) -> None:
    matrix_tag = qname(ns, "matrix")
    matrix = node.find(matrix_tag)
    if matrix is None:
        matrix = ET.SubElement(node, matrix_tag, {"sid": "transform"})
    matrix.set("sid", "transform")
    if not (matrix.text or "").strip():
        matrix.text = IDENTITY_MATRIX


def create_node(ns: str, node_id: str) -> ET.Element:
    node = ET.Element(
        qname(ns, "node"),
        {
            "id": node_id,
            "name": node_id,
            "type": "NODE",
        },
    )
    matrix = ET.SubElement(node, qname(ns, "matrix"), {"sid": "transform"})
    matrix.text = IDENTITY_MATRIX
    return node


def create_colmesh_from_visible(
    ns: str, visible_node: ET.Element, colmesh_id: str, source_url: str
) -> ET.Element:
    node = ET.Element(
        qname(ns, "node"),
        {
            "id": colmesh_id,
            "name": colmesh_id,
            "type": "NODE",
        },
    )

    src_matrix = visible_node.find(qname(ns, "matrix"))
    matrix = ET.SubElement(node, qname(ns, "matrix"), {"sid": "transform"})
    if src_matrix is not None and (src_matrix.text or "").strip():
        matrix.text = (src_matrix.text or "").strip()
    else:
        matrix.text = IDENTITY_MATRIX

    ET.SubElement(
        node,
        qname(ns, "instance_geometry"),
        {
            "url": source_url,
            "name": colmesh_id,
        },
    )
    return node


def main() -> int:
    if "--verify" in sys.argv:
        return verify(dae_file)

    print(f"Processing: {dae_file}")

    try:
        tree = ET.parse(dae_file)
    except ET.ParseError as exc:
        print(f"ERROR: XML parse failed: {exc}")
        return 1
    except FileNotFoundError:
        print(f"ERROR: File not found: {dae_file}")
        return 1

    root = tree.getroot()
    ns = get_namespace(root.tag)
    if ns:
        ET.register_namespace("", ns)

    visual_scene = root.find(f".//{qname(ns, 'visual_scene')}[@id='Scene']")
    if visual_scene is None:
        print("ERROR: Could not find visual_scene id='Scene'")
        return 1

    base_node = find_child_by_id(visual_scene, ns, "base00")
    if base_node is None:
        base_node = create_node(ns, "base00")
        existing_children = [
            child for child in list(visual_scene) if child.tag == qname(ns, "node")
        ]
        for child in existing_children:
            visual_scene.remove(child)
            base_node.append(child)
        visual_scene.append(base_node)
    else:
        ensure_identity_matrix(base_node, ns)

    start_node = find_child_by_id(base_node, ns, "start01")
    if start_node is None:
        start_node = create_node(ns, "start01")
        base_children = [
            child
            for child in list(base_node)
            if child.tag == qname(ns, "node") and child.get("id") != "start01"
        ]
        for child in base_children:
            base_node.remove(child)
            start_node.append(child)
        base_node.append(start_node)
    else:
        ensure_identity_matrix(start_node, ns)

    moved_colmesh = 0
    created_colmesh = 0
    node_tag = qname(ns, "node")
    
    # Check if Colmesh nodes exist at start01 level (they should stay there)
    existing_colmesh_at_start = []
    for child in list(start_node):
        child_id = child.get("id", "")
        if child.tag == node_tag and child_id.startswith("Colmesh-"):
            existing_colmesh_at_start.append(child)

    visible_render_nodes: list[ET.Element] = []
    for child in list(start_node):
        child_id = child.get("id", "")
        if child.tag == node_tag and child_id.startswith("SM_Deco"):
            inst = child.find(qname(ns, "instance_geometry"))
            if inst is not None and inst.get("url"):
                visible_render_nodes.append(child)

    if not existing_colmesh_at_start and visible_render_nodes:
        for i, vis_node in enumerate(visible_render_nodes, start=1):
            inst = vis_node.find(qname(ns, "instance_geometry"))
            if inst is None:
                continue
            source_url = inst.get("url")
            if not source_url:
                continue
            colmesh_id = f"Colmesh-{i}"
            colmesh = create_colmesh_from_visible(ns, vis_node, colmesh_id, source_url)
            start_node.append(colmesh)
            created_colmesh += 1

    total_colmesh = 0
    for child in list(start_node):
        if child.tag == node_tag and child.get("id", "").startswith("Colmesh-"):
            total_colmesh += 1

    visible_nodes = len(visible_render_nodes)

    try:
        ET.indent(tree, space="  ")
    except AttributeError:
        pass

    tree.write(dae_file, encoding="utf-8", xml_declaration=True)

    print("Done.")
    print("Preserved existing visual mesh/material/UV data.")
    print("Ensured hierarchy: base00 > start01 > {SM_Deco (visual) + Colmesh (collision)}")
    print(f"Visible nodes under start01: {visible_nodes}")
    print(f"Colmesh nodes created as siblings under start01: {created_colmesh}")
    print(f"Total Colmesh nodes under start01: {total_colmesh}")
    print(f"Saved: {dae_file}")
    return 0


if __name__ == "__main__":
    sys.exit(main())

const fs = require('fs');
const path = require('path');
const { DOMParser, XMLSerializer } = require('@xmldom/xmldom');

const daeFile = process.argv[2] || 'unpacked/jurassic_beam_park_localfix.disabled/levels/jurassic_beam_park/art/shapes/JP-cage/jp-raptorcagecomplete.dae';

console.log(`Processing: ${daeFile}`);
const xmlText = fs.readFileSync(daeFile, 'utf-8');
const parser = new DOMParser();
const xml = parser.parseFromString(xmlText, 'application/xml');

// Find visual scene
let visualScene = null;
const sceneNode = xml.getElementsByTagName('visual_scene')[0];
if (sceneNode) visualScene = sceneNode;

if (!visualScene) {
  console.error('No visual_scene found');
  process.exit(1);
}

// Find visible instances
const instances = [];
for (const node of visualScene.getElementsByTagName('instance_geometry')) {
  const parent = node.parentNode;
  const url = node.getAttribute('url');
  if (url && url.startsWith('#')) {
    const geomId = url.substring(1);
    const geom = xml.getElementById(geomId);
    if (geom) {
      instances.push({ node: parent, geom, geomId });
    }
  }
}

console.log(`Found ${instances.length} visible mesh instance(s)`);
if (instances.length === 0) {
  process.exit(1);
}

// Create geometry lib if needed
let geometryLib = xml.getElementsByTagName('library_geometries')[0];
if (!geometryLib) {
  geometryLib = xml.createElementNS('http://www.collada.org/2005/11/COLLADASchema', 'library_geometries');
  xml.documentElement.insertBefore(geometryLib, xml.documentElement.getElementsByTagName('library_visual_scenes')[0]);
}

// Collect existing IDs
const existingIds = new Set();
for (const elem of xml.getElementsByTagName('*')) {
  const id = elem.getAttribute('id');
  if (id) existingIds.add(id);
}

// Ensure collision container (base00/start01)
let targetContainer = null;
if (instances[0].node === visualScene || instances[0].node.parentNode === visualScene) {
  const base00 = xml.createElement('node');
  base00.setAttribute('id', 'base00');
  base00.setAttribute('name', 'base00');
  base00.setAttribute('type', 'NODE');
  const matrix = xml.createElement('matrix');
  matrix.setAttribute('sid', 'transform');
  matrix.textContent = '1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1';
  base00.appendChild(matrix);

  const start01 = xml.createElement('node');
  start01.setAttribute('id', 'start01');
  start01.setAttribute('name', 'start01');
  start01.setAttribute('type', 'NODE');
  const matrix2 = xml.createElement('matrix');
  matrix2.setAttribute('sid', 'transform');
  matrix2.textContent = '1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1';
  start01.appendChild(matrix2);

  // Move existing nodes into start01
  const nodesToMove = Array.from(visualScene.childNodes).filter(n => n.nodeType === 1 && n.localName === 'node');
  for (const node of nodesToMove) {
    start01.appendChild(node);
  }

  base00.appendChild(start01);
  visualScene.appendChild(base00);
  targetContainer = start01;
}

// Create collision layer
const collisionLayer = xml.createElement('node');
collisionLayer.setAttribute('id', 'collision-1');
collisionLayer.setAttribute('name', 'collision-1');
collisionLayer.setAttribute('type', 'NODE');
const matrix3 = xml.createElement('matrix');
matrix3.setAttribute('sid', 'transform');
matrix3.textContent = '1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1';
collisionLayer.appendChild(matrix3);

// Clone geometry for collision and create Colmesh
for (let i = 0; i < instances.length; i++) {
  const inst = instances[i];
  const sourceGeom = inst.geom;
  const sourceGeomId = sourceGeom.getAttribute('id');
  
  const colGeomId = `Colmesh-${i + 1}-mesh`;
  const colNodeId = `Colmesh-${i + 1}`;
  
  // Clone geometry
  const clonedGeom = sourceGeom.cloneNode(true);
  clonedGeom.setAttribute('id', colGeomId);
  clonedGeom.setAttribute('name', `Colmesh-${i + 1}`);
  
  // Remap mesh references in cloned geometry
  for (const mesh of clonedGeom.getElementsByTagName('mesh')) {
    for (const source of mesh.getElementsByTagName('source')) {
      const id = source.getAttribute('id');
      if (id && id.startsWith(sourceGeomId)) {
        const newId = id.replace(sourceGeomId, colGeomId);
        source.setAttribute('id', newId);
        for (const input of source.parentElement.getElementsByTagName('input')) {
          if (input.getAttribute('source') === '#' + id) {
            input.setAttribute('source', '#' + newId);
          }
        }
        for (const polylist of source.parentElement.getElementsByTagName('polylist')) {
          for (const input of polylist.getElementsByTagName('input')) {
            if (input.getAttribute('source') === '#' + id) {
              input.setAttribute('source', '#' + newId);
            }
          }
        }
      }
    }
  }
  
  geometryLib.appendChild(clonedGeom);
  
  // Create Colmesh node
  const colNode = xml.createElement('node');
  colNode.setAttribute('id', colNodeId);
  colNode.setAttribute('name', `Colmesh-${i + 1}`);
  colNode.setAttribute('type', 'NODE');
  
  // Copy transform from visible mesh
  const sourceTransform = inst.node.getElementsByTagName('matrix')[0];
  if (sourceTransform) {
    const transformClone = sourceTransform.cloneNode(true);
    colNode.appendChild(transformClone);
  } else {
    const identityMatrix = xml.createElement('matrix');
    identityMatrix.setAttribute('sid', 'transform');
    identityMatrix.textContent = '1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1';
    colNode.appendChild(identityMatrix);
  }
  
  // Add geometry instance
  const geomInstance = xml.createElement('instance_geometry');
  geomInstance.setAttribute('url', '#' + colGeomId);
  geomInstance.setAttribute('name', `Colmesh-${i + 1}`);
  colNode.appendChild(geomInstance);
  
  collisionLayer.appendChild(colNode);
}

// Add collision layer to scene
const parent = targetContainer || visualScene;
parent.appendChild(collisionLayer);

// Write output
const serializer = new XMLSerializer();
const outputXml = serializer.serializeToString(xml);
fs.writeFileSync(daeFile, outputXml, 'utf-8');

console.log(`✓ Added collision layer with ${instances.length} Colmesh node(s)`);
console.log(`✓ Created base00/start01 container hierarchy`);
console.log(`✓ Grouped collision meshes under collision-1 layer`);
console.log(`✓ Saved to ${daeFile}`);

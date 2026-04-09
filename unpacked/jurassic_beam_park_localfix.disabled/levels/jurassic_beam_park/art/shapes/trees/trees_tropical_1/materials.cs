singleton Material(tro_tree_1_trunk)
{
    mapTo = "tro_tree_1_trunk";
    diffuseColor[0] = "0.94902 0.87451 0.788235 1";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/trees/trees_tropical_1/trunk_d.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "0";
    doubleSided = "0";
    alphaTest = "0";
    alphaRef = "1";
    materialTag1 = "vegetation";
    materialTag0 = "beamng";
    specular[0] = "1 1 1 1";
    subSurface[0] = "0";
    subSurfaceColor[0] = "1 0.2 0.2 1";
    subSurfaceRolloff[0] = "0.2";
    materialTag2 = "Natural";
    specularStrength[0] = "0.392157";
   detailMap[0] = "levels/jurassic_beam_park/art/shapes/trees/trees_tropical_1/trunk_variation_1.dds";
   detailScale[0] = "0.1 0.1";
};

singleton Material(tro_tree_1_leaves)
{
    mapTo = "tro_tree_1_leaves";
    alphaRef = "50";
    materialTag1 = "vegetation";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    alphaTest = "1";
    doubleSided = "1";
    specularPower[0] = "128";
    materialTag2 = "Natural";
    materialTag3 = "vegetation";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/trees/trees_tropical_1/tro_leaves_01_n.dds";
    specularStrength[0] = "0.882353";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/trees/trees_tropical_1/tro_leaves_01_s.dds";
    diffuseColor[0] = "0.882353 0.882353 0.878431 1.256";
   colorMap[0] = "levels/jurassic_beam_park/art/shapes/trees/trees_tropical_1/tro_leaves_01_d.dds";
   specularStrength0 = "0.882353";
};

singleton Material(tro_tree_1_branches)
{
    mapTo = "tro_tree_1_branches";
    diffuseMap[0] = "tro_tree_1_branches_d.dds";
    normalMap[0] = "tro_tree_1_branches_n.dds";
    specularMap[0] = "tro_tree_1_branches_s.dds";
    alphaRef = "50";
    materialTag1 = "vegetation";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    alphaTest = "1";
    specularPower[0] = "1";
    materialTag2 = "Natural";
    materialTag3 = "vegetation";
};

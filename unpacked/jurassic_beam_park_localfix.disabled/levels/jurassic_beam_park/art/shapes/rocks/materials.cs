
singleton Material(rock_01_a)
{
    mapTo = "rocks_01_a";
    diffuseMap[0] = "rocks_01_a_d";
    normalMap[0] = "rocks_01_a_n";
    specularMap[0] = "rocks_01_a_s.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag0 = "beamng";
    materialTag1 = "Natural";
};

singleton Material(terrain_rock)
{
    mapTo = "terrain_rock";
    diffuseColor[0] = "0.996078 0.996078 0.996078 2";
    diffuseMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-D.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-S.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-N2.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    materialTag1 = "Natural";
    materialTag0 = "beamng";
    detailScale[0] = "2 2";
    detailNormalMapStrength[0] = "1";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    specularStrength[0] = "0.0980392";
};

singleton Material(terrain_rock_dark)
{
    mapTo = "terrain_rock_dark";
    diffuseColor[0] = "0.552941 0.552941 0.552941 1";
    diffuseMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-D.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-N.dds";
    doubleSided = "0";
    translucentBlendOp = "None";
    materialTag1 = "Natural";
    materialTag0 = "beamng";
    detailScale[0] = "5 5";
    detailNormalMapStrength[0] = "1";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    specularMap[0] = "levels/jurassic_beam_park/art/terrains/Rock-05-D.dds";
    specularStrength[0] = "0.392157";
};

singleton Material(sign1)
{
    mapTo = "sign1_tex";
    diffuseMap[0] = "sign1_tex.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    materialTag0 = "conklin";
    materialTag1 = "Industrial";
};

singleton Material(dino)
{
    mapTo = "dino";
    diffuseMap[0] = "dino.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    materialTag0 = "conklin";
    materialTag1 = "Industrial";
	translucent = "1";
};

singleton Material(dino1)
{
    mapTo = "dino1";
    diffuseMap[0] = "dino1.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    materialTag0 = "conklin";
    materialTag1 = "Industrial";
	translucent = "1";
};

singleton Material(dino2)
{
    mapTo = "dino2";
    diffuseMap[0] = "dino2.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    materialTag0 = "conklin";
    materialTag1 = "Industrial";
	translucent = "1";
};

singleton Material(gate)
{
    mapTo = "gate_tex";
    diffuseMap[0] = "gate_d.dds";
    specularMap[0] = "gate_s.dds";
    normalMap[0] = "gate_n.dds";
    materialTag0 = "conklin";
    materialTag1 = "Industrial";
};

singleton Material(Concrete_Road_Barrier_a)
{
    mapTo = "concrete_road_barrier_a";
    diffuseMap[0] = "concrete_road_barrier_a_d.dds";
    specularMap[0] = "concrete_road_barrier_a_s.dds";
    normalMap[0] = "concrete_road_barrier_a_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    diffuseColor[0] = "0.992157 0.992157 0.992157 1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
};

singleton Material(animals)
{
    mapTo = "animals";
    diffuseMap[0] = "animals_diff.dds";
    specularMap[0] = "animals_spec.dds";
    normalMap[0] = "animals_n.dds";
 };

singleton Material(terrain_grass2)
{
    mapTo = "terrain_grass2";
    diffuseColor[0] = "0.87451 0.854902 0.639216 1";
    diffuseMap[0] = "levels/jurassic_beam_park/art/terrains/grass-02-d.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/terrains/grass-02-n.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag1 = "Natural";
    materialTag0 = "beamng";
};

singleton Material(speedbump_01a)
{
    mapTo = "speedbump_01a";
    diffuseMap[0] = "speedbump_01a_d.dds";
    normalMap[0] = "speedbump_01a_n.dds";
    specularMap[0] = "speedbump_01a_s.dds";
    specularPower[0] = "3";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
};

singleton Material(container_01_b_Material)
{
    mapTo = "Material";
    diffuseColor[0] = "0.64 0.64 0.64 1";
    doubleSided = "1";
    translucentBlendOp = "None";
};

singleton Material(concrete_bridge_01a)
{
    mapTo = "concrete_bridge_01a";
    diffuseMap[0] = "concrete_bridge_01a_d.dds";
    normalMap[0] = "concrete_bridge_01a_n.dds";
    specularMap[0] = "concrete_bridge_01a_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "0.4 0.4 0.4 1";
    diffuseColor[0] = "0.8 0.8 0.8 1";
};

singleton Material(container_01_a_containers_01_a)
{
    mapTo = "containers_01_a";
    diffuseColor[0] = "0.996078 0.992157 0.992157 1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "containers_01_a_d.dds";
    normalMap[0] = "containers_01_a_n.dds";
    specularMap[0] = "containers_01_a_s.dds";
    useAnisotropic[0] = "1";
};

singleton Material(parkinglot_01_a_terrain_rockydirt)
{
    mapTo = "terrain_rockydirt";
    diffuseColor[0] = "0.835294 0.807843 0.8 1";
    doubleSided = "1";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/terrains/RockyDirt-01-D.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/terrains/RockyDirt-01-N.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/terrains/RockyDirt-01-S.dds";
};

singleton Material(a_asphalt_01_a)
{
    mapTo = "asphalt_01_a";
    diffuseColor[0] = "0.803922 0.803922 0.803922 1";
    diffuseMap[0] = "asphalt_01_a_d.dds";
    specularPower[0] = "1";
    specularMap[0] = "asphalt_01_a_s.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag1 = "RoadAndPath";
    materialTag0 = "beamng";
};

singleton Material(misc_markings_01)
{
    mapTo = "misc_markings_01";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    diffuseMap[0] = "misc_markings.dds";
    specular[0] = "0.5 0.5 0.5 1";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    doubleSided = "1";
    castShadows = "0";
    translucentZWrite = "1";
    alphaTest = "1";
    alphaRef = "40";
    materialTag1 = "Industrial";
    materialTag0 = "beamng";
};

singleton Material(race_checkpoint)
{
    mapTo = "race_checkpoint";
    diffuseMap[0] = "race_checkpoint_texture_d.dds";
    doubleSided = "1";
    translucentBlendOp = "LerpAlpha";
    detailScale[0] = "2 2";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    specular[0] = "0.72549 0.72549 0.72549 1";
    specularPower[0] = "24";
    pixelSpecular[0] = "0";
    diffuseColor[0] = "0.996078 0.0784314 0.00784314 1";
    emissive[0] = "1";
    castShadows = "0";
    translucent = "1";
    alphaRef = "129";
    alphaTest = "0";
    animFlags[0] = "0x00000009";
    scrollDir[0] = "0 -1";
    scrollSpeed[0] = "1.059";
    parallaxScale[0] = "0.125";
    glow[0] = "1";
    rotSpeed[0] = "-0.59";
    waveType[0] = "Triangle";
    waveFreq[0] = "0.156";
    waveAmp[0] = "1";
};

singleton Material(misc_billboard_title)
{
    mapTo = "misc_billboard_title";
    diffuseMap[0] = "billboards_01b.dds";
    detailScale[0] = "10 10";
    specular[0] = "0.5 0.5 0.5 1";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag1 = "beamng";
    materialTag2 = "Miscellaneous";
    materialTag0 = "Industrial";
    materialTag3 = "Natural";
};

singleton Material(ferry_black)
{
    mapTo = "ferry_black";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_black.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ferry_blue)
{
    mapTo = "ferry_blue";
    diffuseMap[0] = "ferry_blue.dds";
    normalMap[0] = "ferry_n.dds";
    specularMap[0] = "ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ferry_hazard)
{
    mapTo = "ferry_hazard";
    diffuseMap[0] = "ferry_hazard.dds";
    normalMap[0] = "ferry_n.dds";
    specularMap[0] = "ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ferry_railing)
{
    mapTo = "ferry_railing";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_railing_d.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_railing_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    alphaTest = "1";
    alphaRef = "50";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
    doubleSided = "1";
};

singleton Material(ferry_red)
{
    mapTo = "ferry_red";
    diffuseMap[0] = "ferry_red.dds";
    normalMap[0] = "ferry_n.dds";
    specularMap[0] = "ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ferry_white)
{
    mapTo = "ferry_white";
    diffuseMap[0] = "ferry_white.dds";
    normalMap[0] = "ferry_n.dds";
    specularMap[0] = "ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "1 1 1 1";
};


singleton Material(ferry_window)
{
    mapTo = "ferry_window";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_white_paint.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_s.dds";
    specularPower[0] = "4";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    specular[0] = "1 1 1 1";
    diffuseColor[0] = "0 0.580392 1 0.008";
    cubemap = "BNG_Sky_02_cubemap";
};



singleton Material(ferry_white_paint)
{
    mapTo = "ferry_white_paint";
    specular[0] = "0.5 0.5 0.5 1";
    specularPower[0] = "50";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_white_paint.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/misc/ferry_s.dds";
    materialTag1 = "Industrial";
    materialTag0 = "beamng";
};

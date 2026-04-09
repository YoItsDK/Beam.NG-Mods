singleton Material(ind_concreteleak)
{
    mapTo = "ind_concreteleak";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "ind_concreteleak_d.dds";
    detailScale[0] = "0.2 0.2";
    normalMap[0] = "ind_concreteleak_n.dds";
    specularMap[0] = "ind_concreteleak_s.dds";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
	annotation = "BUILDINGS";
};

singleton Material(ind_metalplates)
{
    mapTo = "ind_metalplates";
    diffuseColor[0] = "0.815686 0.815686 0.815686 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "ind_metalplates_d.dds";
    detailScale[0] = "0.1 0.1";
    normalMap[0] = "ind_metalplates_n.dds";
    specularMap[0] = "ind_metalplates_s.dds";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
	annotation = "BUILDINGS";
};

singleton Material(utah_caution_tape)
{
   mapTo = "utah_caution_tape";
   doubleSided = "0";
   translucentBlendOp = "None";
   normalMap[0] = "ut_caution_tape_barricade_n.dds";
   specularMap[0] = "ut_caution_tape_barricade_s.dds";
   specularPower[0] = "1";
   useAnisotropic[0] = "1";
   materialTag0 = "beamng";
   materialTag1 = "race";
   materialTag2 = "utah";
  colorMap[0] = "ut_caution_tape_barricade_d.dds";
  detailScale[0] = "0.1 0.1";
  specularStrength0 = "0.196078";
};

singleton Material(utah_caution_tape_poles)
{
   mapTo = "utah_caution_tape_poles";
   doubleSided = "0";
   translucentBlendOp = "None";
   normalMap[0] = "ut_caution_tape_barricade_poles_n.dds";
   specularMap[0] = "ut_caution_tape_barricade_poles_s.dds";
   specularPower[0] = "1";
   useAnisotropic[0] = "1";
   materialTag0 = "beamng";
   materialTag1 = "race";
   materialTag2 = "utah";
  colorMap[0] = "ut_caution_tape_barricade_poles_d.dds";
  detailScale[0] = "0.1 0.1";
  specularStrength0 = "0.196078";
};

singleton Material(barrelmarker)
{
    mapTo = "barrelmarker";
    diffuseMap[0] = "barrelmarker_d.dds";
    specularMap[0] = "barrelmarker_s.dds";
    normalMap[0] = "barrelmarker_n.dds";
    reflectivityMap[0] = "barrelmarker_r.dds";
    specularPower[0] = "15";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    cubemap = "global_cubemap_metalblurred";
    materialTag0 = "beamng";
    materialTag1 = "Race";
};

singleton Material(race_rally_start_finish)
{
    mapTo = "race_rally_start_finish";
    diffuseMap[0] = "race_rally_start_finish_d.dds";
    normalMap[0] = "race_rally_start_finish_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "0.992157 0.992157 0.992157 1";
    specularStrength[0] = "0.588235";
    materialTag0 = "beamng";
    materialTag1 = "Race";
    materialTag1 = "rally";
};

singleton Material(race_checkered)
{
    mapTo = "race_checkered";
    diffuseMap[0] = "race_checkered_d.dds";
    normalMap[0] = "race_checkered_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "0.992157 0.992157 0.992157 1";
    specularStrength[0] = "0.588235";
    materialTag0 = "beamng";
    materialTag1 = "Race";
    materialTag1 = "rally";
};

singleton Material(race_rally_finish_checkpoints)
{
    mapTo = "race_rally_finish_checkpoints";
    diffuseMap[0] = "race_rally_finish_checkpoints_d.dds";
    normalMap[0] = "race_rally_finish_checkpoints_n.dds";
    specularPower[0] = "39";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "0.992157 0.992157 0.992157 1";
    specularStrength[0] = "0.588235";
    materialTag0 = "beamng";
    materialTag1 = "rally";
    materialTag1 = "rally";
    doubleSided = "1";
};

singleton Material(checkpoint_sign)
{
    mapTo = "checkpoint_sign";
    diffuseMap[0] = "checkpoint_sign.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/Concrete-01_s.dds";
    specularPower[0] = "15";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Race";
    materialTag1 = "rally";
};


singleton Material(sign_arrows)
{
    mapTo = "sign_arrows";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.5 0.5 0.5 1";
    specularPower[0] = "50";
    translucentBlendOp = "None";
    diffuseMap[0] = "arrows_sign_d.dds";
    specularMap[0] = "arrows_sign_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "Race";
    materialTag1 = "rally";
};

singleton Material(race_wood)
{
    mapTo = "race_wood";
    diffuseMap[0] = "wood_d.dds";
    doubleSided = "0";
    translucentBlendOp = "None";
    specularMap[0] = "wood_s.dds";
    normalMap[0] = "wood_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "Race";
    materialTag1 = "rally";
};

singleton Material(eca_bld_metalbeams)
{
    mapTo = "eca_bld_metalbeams";
    diffuseMap[0] = "eca_bld_metalbeams_d.dds";
    doubleSided = "0";
    translucentBlendOp = "None";
    specularMap[0] = "eca_bld_metalbeams_s.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "beamng";
    materialTag1 = "building";
    materialTag2 = "east_coast_usa";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_01_low_desat.dds";
    detailScale[0] = "0.2 0.2";
    normalMap[0] = "eca_bld_metalbeams_n.dds";
};

singleton Material(eca_bld_commercial_doors)
{
    mapTo = "eca_bld_commercial_doors";
    diffuseMap[0] = "eca_bld_commercial_doors_a.dds";
    doubleSided = "0";
    translucentBlendOp = "None";
    normalMap[0] = "eca_bld_commercial_doors_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "0";
    materialTag0 = "beamng";
    materialTag1 = "building";
    materialTag2 = "east_coast_usa";
    diffuseMap[1] = "eca_bld_commercial_doors_d.dds";
    cubemap = "BNG_Sky_02_cubemap";
    specularMap[1] = "eca_bld_commercial_doors_s.dds";
    specularMap[0] = "eca_bld_commercial_doors_s.dds";
    specularPower[1] = "1";
};

singleton Material(old_stone_wall)
{
    mapTo = "old_stone_wall";
    diffuseColor[0] = "0.815686 0.815686 0.815686 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/old_stone_wall_d.dds";
    detailScale[0] = "0.1 0.1";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/old_stone_wall_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/old_stone_wall_s.dds";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02.dds";
};

singleton Material(busstop)
{
    mapTo = "busstop";
    vertColor[0] = "1";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_d.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_n.dds";
    reflectivityMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_r.dds";
    specularPower[0] = "32";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "1 1 1 1";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "0";
    emissive[0] = "0";
    glow[0] = "0";
    translucentBlendOp = "None";
    cubemap = "global_cubemap_metalblurred";
    alphaTest = "1";
    alphaRef = "64";
};

singleton Material(busstop_windows)
{
    mapTo = "busstop_windows";
    vertColor[0] = "0";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_d.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_n.dds";
    reflectivityMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/busstop_r.dds";
    specularPower[0] = "32";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "1 1 1 1";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    emissive[0] = "0";
    glow[0] = "0";
    translucentBlendOp = "LerpAlpha";
    translucentZWrite = "1";
    cubemap = "global_cubemap_metalblurred";
    alphaTest = "0";
    alphaRef = "64";
};

singleton Material(old_covered_wall)
{
    mapTo = "old_covered_wall";
    diffuseColor[0] = "0.815686 0.815686 0.815686 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/old_covered_wall_d.dds";
    detailScale[0] = "0.2 0.2";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/old_covered_wall_macro.dds";
};

singleton Material(ind_bld_extras2)
{
    mapTo = "ind_bld__2__extras2";
    diffuseMap[0] = "ind_bld_buildingextras2_d.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    detailScale[0] = "2 2";
    specularMap[0] = "ind_bld_buildingextras2_s.dds";
    materialTag0 = "beamng";
    useAnisotropic[0] = "0";
};

singleton Material(ind_bld_Tin_01)
{
    mapTo = "ind_bld__2__Tin-01";
    diffuseMap[0] = "tin-01_d.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    specularMap[0] = "tin-01_s.dds";
    materialTag0 = "Industrial";
    detailMap[0] = "detail_grunge_02_streak.dds";
    detailScale[0] = "0.1 0.1";
    normalMap[0] = "tin-01_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag1 = "beamng";
};

singleton Material(metal_cage)
{
    mapTo = "metal_cage";
    diffuseColor[0] = "0.64 0.64 0.64 1";
    doubleSided = "0";
    translucent = "0";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/metal_cage_d.dds";
    useAnisotropic[0] = "1";
    alphaTest = "1";
    alphaRef = "140";
    materialTag1 = "beamng";
    materialTag0 = "beamng";
    materialTag2 = "Industrial";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/metal_cage_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/metal_cage_s.dds";
    specularPower[0] = "1";
};

singleton Material(beamng_text)
{
    mapTo = "beamng_text";
    diffuseMap[0] = "beamng_text.dds";
    specularPower[0] = "21";
    pixelSpecular[0] = "1";
    diffuseColor[0] = "0.992157 0.992157 0.992157 1";
    materialTag0 = "beamng"; materialTag1 = "object";
    alphaTest = "1";
    alphaRef = "120";
    castShadows = "0";
};

singleton Material(ind_bld_tin_02)
{
    mapTo = "ind_bld__2__Tin-02";
    diffuseMap[0] = "tin-01b_d.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag0 = "Industrial";
    normalMap[0] = "tin-01_n.dds";
    specularMap[0] = "tin-01b_s.dds";
    detailScale[0] = "0.1 0.1";
    detailMap[0] = "detail_grunge_02_streak.dds";
    specularPower[0] = "8";
    useAnisotropic[0] = "1";
    detailNormalMapStrength[0] = "0.1";
    materialTag1 = "beamng";
};

singleton Material(ind_bld_extras)
{
    mapTo = "ind_bld__2__extras";
    diffuseMap[0] = "ind_bld_buildingextras_d.dds";
    doubleSided = "0";
    translucentBlendOp = "None";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    specularMap[0] = "ind_bld_buildingextras_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
};

singleton Material(ind_bld_brick_01)
{
    mapTo = "Ind_bld__2__bricks";
    diffuseMap[0] = "ind_bld_brick_02_d.dds";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02b.dds";
    normalMap[0] = "ind_bld_brick_02_n.dds";
    specularMap[0] = "ind_bld_brick_02_s.dds";
    detailScale[0] = "0.1 0.1";
    specularPower[0] = "1";
    specularStrength[0] = "1.3";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    materialTag2 = "Industrial";
};
singleton Material(ind_wood_crate)
{
    mapTo = "ind_wood_crate";
    diffuseMap[0] = "WoodCrate-01_d";
    normalMap[0] = "WoodCrate-01_n";
    doubleSided = "1";
    translucentBlendOp = "None";
};

singleton Material(ind_bld_rustymetal_01)
{
    mapTo = "ind_bld__2__rustymetal_01";
    diffuseMap[0] = "ind_bld_rustymetal_01_d.dds";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02.dds";
    detailScale[0] = "0.1 0.1";
    normalMap[0] = "ind_bld_rustymetal_01_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    specularMap[0] = "ind_bld_rustymetal_01_s.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    materialTag0 = "Industrial";
    materialTag1 = "beamng";
};

singleton Material(ind_bld_metalgrate_01)
{
    mapTo = "ind_bld_metalgrate_01";
    diffuseMap[0] = "ind_bld_mgrate-01_d.dds";
    normalMap[0] = "ind_bld_mgrate-01_s.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    alphaTest = "1";
    alphaRef = "115";
    materialTag1 = "beamng";
    materialTag0 = "Industrial";
    doubleSided = "1";
    materialTag2 = "Industrial";
};



singleton Material(Asphalt_road_01_decal2)
{
    mapTo = "unmapped_mat";
    diffuseColor[0] = "0.996078 0.996078 0.996078 0.755";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_decal_02.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    materialTag0 = "beamng";
    materialTag2 = "RoadAndPath";
    materialTag1 = "decal";
};

singleton Material(ind_bld_extras3)
{
    mapTo = "ind_bld__2__extras3";
    diffuseMap[0] = "ind_bld_buildingextras3_d.dds";
    specularPower[0] = "1";
    doubleSided = "1";
    castShadows = "0";
    translucent = "1";
    alphaRef = "13";
    materialTag0 = "beamng";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
};

singleton Material(ind_bld_concrete)
{
    mapTo = "ind_bld__2__concrete";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    specularStrength[0] = "0.0980392";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/concrete-01_d.dds";
    detailScale[0] = "0.1 0.1";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/concrete-01_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/Concrete-01_s.dds";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_03.dds";
    specularStrength[0] = "0.588235";
};

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
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02.dds";
};

singleton Material(ind_concrete_02)
{
    mapTo = "ind_concrete_02";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/concrete_02_d.dds";
    detailScale[0] = "0.7 0.7";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/concrete_02_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/concrete_02_s.dds";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02.dds";
    specularStrength[0] = "0.588235";
};

singleton Material(ind_bld_concrete_old)
{
    mapTo = "ind_bld_concrete_old";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "ind_bld_concrete_old_d.dds";
    detailScale[0] = "0.7 0.7";
    normalMap[0] = "ind_bld_concrete_old_n.dds";
    specularMap[0] = "ind_bld_concrete_old_s.dds";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "1";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_02.dds";
    specularStrength[0] = "0.588235";
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
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_03.dds";
};

singleton Material(gas_station_01_floor_tiles_01_a)
{
    mapTo = "floor_tiles_01_a";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "floor_tiles_01_a_d.dds";
    specularMap[0] = "floor_tiles_01_a_s.dds";
};

singleton Material(gas_station_01_metal_01_a)
{
    mapTo = "metal_01_a";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "metal_01_a_d.dds";
    specularMap[0] = "metal_01_a_s.dds";
};

singleton Material(gas_station_01_wall_plaster_01)
{
    mapTo = "wall_plaster_01";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "wall_plaster_01_d.dds";
};

singleton Material(gas_station_01_roof_tiles_01_a)
{
    mapTo = "roof_tiles_01_a";
    diffuseColor[0] = "0.760784 0.760784 0.760784 1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "floor_tiles_01_a_d.dds";
};

singleton Material(gas_station_01_wall_plaster_01_red)
{
    mapTo = "wall_plaster_01_red";
    diffuseColor[0] = "0.745098 0.333333 0.301961 1";
    doubleSided = "1";
    translucentBlendOp = "None";
    diffuseMap[0] = "wall_plaster_01_d.dds";
};

singleton Material(gas_station_01_text_extras)
{
    mapTo = "gas_station_01_text_extras";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    doubleSided = "0";
    translucent = "1";
    diffuseMap[0] = "gas_station_01_text_extras_d.dds";
    castShadows = "0";
    alphaTest = "0";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    parallaxScale[0] = "0.1";
    translucentZWrite = "0";
};

singleton Material(gas_station_01_text_extras_b)
{
    mapTo = "gas_station_01_text_extras_b";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    doubleSided = "0";
    translucent = "0";
    diffuseMap[0] = "gas_station_01_text_extras_d.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
};

singleton Material(ind_bld_05_ColorEffectR27G177B88_material_001)
{
    mapTo = "ColorEffectR27G177B88-material.001";
    diffuseColor[0] = "0.0684948 0.444078 0.221891 1";
    specular[0] = "0.25 0.25 0.25 1";
    specularPower[0] = "50";
    doubleSided = "1";
    translucentBlendOp = "None";
};

singleton Material(ind_walk_Rnd_ind_bld__proxy__physProxyNoDraw)
{
    mapTo = "ind_bld__proxy__physProxyNoDraw";
    diffuseColor[0] = "0.64 0.64 0.64 1";
    specular[0] = "0.5 0.5 0.5 1";
    specularPower[0] = "50";
    doubleSided = "1";
    translucentBlendOp = "None";
};

singleton Material(ind_apt_siding)
{
    mapTo = "ind_apt_siding";
    diffuseMap[0] = "ind_apt_siding_d.dds";
    normalMap[0] = "ind_apt_siding_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_apt_concrete)
{
    mapTo = "ind_apt_concrete";
    diffuseMap[0] = "ind_apt_concrete_d.dds";
    normalMap[0] = "ind_apt_concrete_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_apt_roof)
{
    mapTo = "ind_apt_roof";
    diffuseMap[0] = "ind_apt_roof_d.dds";
    normalMap[0] = "ind_apt_roof_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_apt_ceiling)
{
    mapTo = "ind_apt_ceiling";
    diffuseMap[0] = "ind_apt_ceiling_d.dds";
    normalMap[0] = "ind_apt_ceiling_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_apt_window)
{
    mapTo = "ind_apt_window";
    diffuseMap[1] = "ind_apt_window_d.dds";
    specularMap[1] = "ind_apt_window_s.dds";
    normalMap[1] = "ind_apt_window_n.dds";
    diffuseMap[0] = "null.dds";
    specularMap[0] = "null.dds";
    normalMap[0] = "ind_apt_window_n.dds";
    specularPower[0] = "10";
    pixelSpecular[0] = "1";
    specularPower[1] = "10";
    pixelSpecular[1] = "1";
    diffuseColor[0] = "1 1 1 1";
    diffuseColor[1] = "1 1 1 1";
    castShadows = "1";
    translucent = "1";
    translucentBlendOp = "None";
    alphaTest = "1";
    alphaRef = "0";
    cubemap = "BNG_Sky_02_cubemap";
    materialTag0 = "beamng";
};

singleton Material(ind_apt_door)
{
    mapTo = "ind_apt_door";
    diffuseMap[0] = "ind_apt_door_d.dds";
    normalMap[0] = "ind_apt_door_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_apt_trim)
{
    mapTo = "ind_apt_trim";
    diffuseMap[0] = "ind_apt_trim_d.dds";
    normalMap[0] = "ind_apt_trim_n.dds";
    materialTag0 = "beamng";
    diffuseColor[0] = "1 1 1 1";
};

singleton Material(ind_bld_tin_01_a_ind_bld__2__rustymetal_01_001)
{
    mapTo = "ind_bld__2__rustymetal_01_001";
    diffuseColor[0] = "0.8 0.8 0.8 1";
    doubleSided = "1";
    translucentBlendOp = "None";
};

singleton Material(pier_support)
{
    mapTo = "pier_support";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "15";
    doubleSided = "1";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_d.dds";
    detailScale[0] = "0.1 0.1";
    detailNormalMapStrength[0] = "1";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_n.dds";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/ruinedpier_support_d.dds";
};

singleton Material(wood_01)
{
    mapTo = "wood_01";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "19";
    doubleSided = "1";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_d.dds";
    detailScale[0] = "0.1 0.1";
    detailNormalMapStrength[0] = "1";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_n.dds";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_overlay.dds";
};

singleton Material(littleboat_01_ab)
{
    mapTo = "littleboat_01_ab";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "19";
    doubleSided = "1";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/littleboat_01_ab_d.dds";
    detailScale[0] = "8 8";
    detailNormalMapStrength[0] = "0.5";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_d.dds";
    detailNormalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/wood_01_n.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/littleboat_01_ab_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/littleboat_01_ab_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "Natural";
};

singleton Material(ind_obj_04_b_ColorEffectR225G87B143_material)
{
    mapTo = "ColorEffectR225G87B143-material";
    diffuseColor[0] = "0.562958 0.218825 0.357338 1";
    specular[0] = "0.25 0.25 0.25 1";
    specularPower[0] = "50";
    translucentBlendOp = "None";
};

singleton Material(metal_01)
{
    mapTo = "metal_01";
    diffuseMap[0] = "metal_01_a_d.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    specularMap[0] = "metal_01_a_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    normalMap[0] = "metal_01_a_n.dds";
    detailMap[0] = "detail_grunge_01.dds";
    detailScale[0] = "0.2 0.2";
};

singleton Material(metal_01_orange)
{
    mapTo = "metal_01_orange";
    diffuseMap[0] = "metal_01_a_orange_d.dds";
    doubleSided = "1";
    translucentBlendOp = "None";
    specularMap[0] = "metal_01_a_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
    normalMap[0] = "metal_01_a_n.dds";
    detailMap[0] = "detail_grunge_01.dds";
    detailScale[0] = "0.1 0.1";
};

singleton Material(ind_tunnel_mountain_light)
{
    mapTo = "ind_tunnel_mountain_light";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularPower[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/ind_tunnel_mountain_light_d.dds";
    detailScale[0] = "0.1 0.1";
    materialTag2 = "Industrial";
    materialTag0 = "beamng";
    useAnisotropic[0] = "0";
    emissive[0] = "1";
    subSurface[0] = "1";
    glow[0] = "1";
};

singleton Material(jri_port_edge)
{
    mapTo = "jri_port_edge";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/jri_port_edge_d.dds";
    detailMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/detail_grunge_03.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/jri_port_edge_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/jri_port_edge_s.dds";
    detailScale[0] = "0.1 0.1";
    specularPower[0] = "1";
    specularStrength[0] = "0.196078";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    doubleSided = "0";
    translucentBlendOp = "None";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
    materialTag2 = "Industrial";
};

singleton Material(jri_bridge_01_asphaltroad_laned_nolines_mesh)
{
    mapTo = "unmapped_mat";
    diffuseColor[0] = "0.64 0.64 0.64 1";
    translucentBlendOp = "None";
    diffuseMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_nolines_d.dds";
    materialTag0 = "beamng";
    materialTag2 = "Industrial";
};

singleton Material(asphaltroad_laned_nolines_mesh)
{
    mapTo = "asphaltroad_laned_nolines_mesh";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_a.dds";
    diffuseMap[1] = "sphaltroad_laned_nolines_d.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    specularPower[1] = "1";
    specularMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    useAnisotropic[0] = "1";
    useAnisotropic[1] = "1";
    translucentBlendOp = "None";
    alphaRef = "255";
    cubemap = "cubemap_road_sky_reflection";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "Industrial";
};

singleton Material(ind_planks_01)
{
    mapTo = "Ind_planks";
    diffuseMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/ind_planks_01_d.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/ind_planks_01_n.dds";
    specularPower[0] = "1";
    specularStrength[0] = "0.294118";
    specularMap[0] = "levels/jurassic_beam_park/art/shapes/buildings/ind_planks_01_s.dds";
    useAnisotropic[0] = "1";
    alphaTest = "1";
    alphaRef = "121";
    materialTag0 = "beamng";
    materialTag1 = "Industrial";
};

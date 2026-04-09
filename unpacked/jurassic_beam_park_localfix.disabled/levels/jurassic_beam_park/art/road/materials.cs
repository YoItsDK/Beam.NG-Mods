singleton Material(road_markings_line_thin)
{
    mapTo = "road_markings_line_thin";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "markings";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    useAnisotropic[0] = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.27451 0.27451 0.27451 1";
    scrollSpeed[0] = "4.118";
    specularStrength[0] = "0.490196";
   colorMap[0] = "road_markings_line_thin_d.dds";
   specularStrength0 = "0.490196";
};

singleton Material(checkered_line)
{
    mapTo = "checkered_line";
    diffuseMap[0] = "checkered_line_d.dds";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "128";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    materialTag2 = "RoadAndPath";
    specularStrength[0] = "0.882353";
    alphaRef = "208";
};

singleton Material(BNG_Asphalt_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_decal_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "decal";
    diffuseMap[1] = "AsphaltRoad_decal_01.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_s.dds";
    specularMap[0] = "AsphaltRoad_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(AsphaltRoad_variation_01)
{
    mapTo = "AsphaltRoad_variation_01";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "128";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
    diffuseColor[0] = "0.847059 0.847059 0.847059 1.248";
    normalMap[0] = "levels/jurassic_beam_park/art/null_n.dds";
   colorMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_variation_01_d.dds";
   pixelSpecular[0] = "1";
   specularStrength0 = "0";
};

singleton Material(AsphaltRoad_lanes_clear)
{
    mapTo = "unmapped_mat";
    materialTag0 = "RoadAndPath";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "8";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "1 1 1 1";
    specularStrength[0] = "0.784314";
    normalMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_clear_n.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_clear_s.dds";
    materialTag2 = "beamng";
    diffuseColor[0] = "0.996078 0.996078 0.996078 1.157";
   colorMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_clear_d.dds";
   specularStrength0 = "0.784314";
};

singleton Material(BNG_Road_Dirt)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/road-01_d.dds";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    normalMap[0] = "levels/jurassic_beam_park/art/road/road-01_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "1";
    specularMap[0] = "levels/jurassic_beam_park/art/road/road-01_s.dds";
    useAnisotropic[0] = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.00392157 0.00392157 0.00392157 1";
    scrollSpeed[0] = "4.118";
    diffuseColor[0] = "0.85098 0.85098 0.85098 0.934";
    specularStrength[0] = "0.196078";
    materialTag2 = "Natural";
    materialTag3 = "vegetation";
};

singleton Material(Road_Dirt_Coastal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/road-03_d.dds";
    materialTag0 = "BeamNG";
    materialTag1 = "RoadAndPath";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    normalMap[0] = "levels/jurassic_beam_park/art/road/road-03_n.dds";
    diffuseColor[0] = "0.894118 0.701961 0.65098 2";
    materialTag2 = "Natural";
    materialTag3 = "vegetation";
};

singleton Material(Road_Dirt_Coastal_Decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-03_decal.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    materialTag1 = "RoadAndPath";
    materialTag0 = "beamng";
    materialTag2 = "decal";
};


singleton Material(Road_01_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-01_decals.dds";
    materialTag0 = "RoadAndPath";
    translucent = "1";
    materialTag1 = "beamng";
    specular[0] = "0.992157 0.992157 0.992157 1";
    specularPower[0] = "1";
    specularMap[0] = "Road-01_s.dds";
    translucentZWrite = "1";
    castShadows = "0";
};

singleton Material(Asphalt)
{
    mapTo = "unmapped_mat";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "levels/jurassic_beam_park/art/road/AsphaltRoad_s.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
    specularStrength[0] = "0";
   colorMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_a.dds";
   colorMap[1] = "levels/jurassic_beam_park/art/road/AsphaltRoad_d.dds";
   specularStrength0 = "0";
};

singleton Material(BNG_Asphalt_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_decal_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "decal";
    diffuseMap[1] = "AsphaltRoad_decal.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_s.dds";
    specularMap[0] = "AsphaltRoad_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(DirtRoad_variation_01)
{
    mapTo = "DirtRoad_variation_01";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/DirtRoad_variation_01_d.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "11";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
    diffuseColor[0] = "0.517647 0.439216 0.294118 0.752";
    materialTag2 = "Natural";
    materialTag3 = "vegetation";
    specularStrength[0] = "0.196078";
    pixelSpecular[0] = "1";
};

singleton Material(DirtRoad_lighten)
{
    mapTo = "DirtRoad_lighten";
    diffuseMap[0] = "DirtRoad_lighten_d.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "16";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
    diffuseColor[0] = "0.384314 0.376471 0.368627 1.752";
    specularStrength[0] = "0.0980392";
    pixelSpecular[0] = "1";
};

singleton Material(AsphaltRoad_damage_edge)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_edge_d.dds";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_edge_s.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    materialTag2 = "RoadAndPath";
    specularStrength[0] = "0.490196";
    normalMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_edge_n.dds";
    diffuseColor[0] = "0.745098 0.745098 0.745098 1.711";
};

singleton Material(road_dirt_top)
{
    mapTo = "unmapped_mat";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "44";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "White";
    specularStrength[0] = "1.47059";
    diffuseColor[0] = "0.647059 0.6 0.560784 1.19";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/road_dirt_top_d.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/road_dirt_top_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/road/road_dirt_top_n.dds";
    alphaRef = "120";
    materialTag2 = "RoadAndPath";
};

singleton Material(mmatting_runway)
{
    mapTo = "unmapped_mat";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "White";
    specularStrength[0] = "0.392157";
    diffuseColor[0] = "0.996078 0.996078 0.996078 2";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/mmatting_runway_d.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/mmatting_runway_s.dds";
    normalMap[0] = "levels/jurassic_beam_park/art/road/mmatting_runway_n.dds";
    alphaRef = "75";
    materialTag2 = "RoadAndPath";
};

singleton Material(AsphaltRoad_lanes)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "asphaltroad_laned_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "asphaltroad_laned_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "asphaltroad_laned_s.dds";
    specularMap[0] = "asphaltroad_laned_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(AsphaltRoad_laned_mesh)
{
    mapTo = "AsphaltRoad_laned_mesh";
    diffuseMap[0] = "asphaltroad_laned_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "asphaltroad_laned_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "asphaltroad_laned_s.dds";
    specularMap[0] = "asphaltroad_laned_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "0";
    translucentZWrite = "0";
    castShadows = "0";
};

singleton Material(AsphaltRoad_lanes_narrow_unbroken)
{
    mapTo = "unmapped_mat";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
   colorMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_a.dds";
   colorMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_unbroken_d.dds";
   normalMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_n.dds";
};

singleton Material(AsphaltRoad_lanes_narrow_unbroken)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "asphaltroad_laned_narrow_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "asphaltroad_laned_narrow_unbroken_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(AsphaltRoad_lanes_narrow_halfbroken)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "asphaltroad_laned_narrow_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "asphaltroad_laned_narrow_halfbroken_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(asphaltroad_lanes_narrow_nolines)
{
    mapTo = "unmapped_mat";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_n.dds";
    specularPower[0] = "128";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
   colorMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_a.dds";
   colorMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_nolines_d.dds";
};

singleton Material(asphaltroad_lanes_narrow_nolines)
{
    mapTo = "unmapped_mat";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
   colorMap[0] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_narrow_a.dds";
   colorMap[1] = "levels/jurassic_beam_park/art/road/asphaltroad_laned_nolines_d.dds";
};

singleton Material(asphaltroad_lanes_narrow_nolines_mesh)
{
    mapTo = "asphaltroad_lanes_narrow_nolines_mesh";
    diffuseMap[0] = "asphaltroad_laned_narrow_a.dds";
    doubleSided = "0";
    normalMap[0] = "asphaltroad_laned_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "asphaltroad_laned_nolines_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "asphaltroad_laned_narrow_s.dds";
    specularMap[0] = "asphaltroad_laned_narrow_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "1";
};


singleton Material(AsphaltRoad_track)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_track_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    normalMap[0] = "AsphaltRoad_track_n.dds";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "AsphaltRoad_track_d.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_track_s.dds";
    specularMap[0] = "AsphaltRoad_track_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(hr_track_edging)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "hr_track_edging_d.dds";
    normalMap[0] = "hr_track_edging_n.dds";
    specularMap[0] = "hr_track_edging_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.996078 0.996078 0.996078 1";
    specularStrength[0] = "0.882353";
};

singleton Material(AsphaltRoad_track_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_track_decal_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "AsphaltRoad_track_decal.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_track_s.dds";
    specularMap[0] = "AsphaltRoad_track_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(AsphaltRoad_TireTracks_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_TireTracks_decal_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    diffuseMap[1] = "AsphaltRoad_TireTracks_decal.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_track_s.dds";
    specularMap[0] = "AsphaltRoad_track_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(dirt_road_tread_ruts)
{
    mapTo = "unmapped_mat";
    specularMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_tread_ruts_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "White";
    specularStrength[0] = "0.0980392";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_tread_ruts_d.dds";
    alphaRef = "134";
    normalMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_tread_ruts_n.dds";
    diffuseColor[0] = "0.933 0.82 0.729 1.207";
};

singleton Material(dirt_road_base)
{
    mapTo = "unmapped_mat";
    specularMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_base_s.dds";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    castShadows = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "White";
    specularStrength[0] = "0.392157";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_base_d.dds";
    alphaRef = "134";
    normalMap[0] = "levels/jurassic_beam_park/art/road/dirt_road_base_n.dds";
    diffuseColor[0] = "0.843137 0.737255 0.662745 1.876";
};

singleton Material(AsphaltRoad_TireTracks_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_TireTracks_decal.dds";
    materialTag1 = "RoadAndPath";
    materialTag0 = "beamng";
    specularMap[0] = "AsphaltRoad_track_s.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    subSurface[0] = "1";
    materialTag2 = "RoadAndPath";
};

singleton Material(BNG_Road_02_decal)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-02_decal.dds";
    materialTag1 = "decal";
    materialTag2 = "RoadAndPath";
    materialTag0 = "beamng";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    specularMap[0] = "road-02_s.dds";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
};


singleton Material(BNG_Road_Dirt_Wide)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-02_d.dds";
    normalMap[0] = "road-02_n.dds";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    specularMap[0] = "road-02_s.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    materialTag0 = "beamng";
    materialTag1 = "RoadAndPath";
};


singleton Material(AsphaltRoad_damage_sml_decal_01)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_damage_sml_decal_01.dds";
    materialTag0 = "beamng";
    materialTag1 = "decal";
    materialTag2 = "RoadAndPath";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    useAnisotropic[0] = "1";
};

singleton Material(AsphaltRoad_damage_large_decal_01)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "AsphaltRoad_damage_large_decal_01_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "decal";
    diffuseMap[1] = "AsphaltRoad_damage_large_decal_01.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "AsphaltRoad_damage_large_decal_01_s.dds";
    specularMap[0] = "AsphaltRoad_damage_large_decal_01_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
};

singleton Material(AsphaltRoad_damage_large_decal_02)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_large_decal_02_a.dds";
    doubleSided = "0";
    translucentBlendOp = "LerpAlpha";
    specularPower[0] = "1";
    useAnisotropic[0] = "1";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    materialTag2 = "decal";
    diffuseMap[1] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_large_decal_02.dds";
    cubemap = "cubemap_road_sky_reflection";
    specularMap[1] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_large_decal_02_s.dds";
    specularMap[0] = "levels/jurassic_beam_park/art/road/AsphaltRoad_damage_large_decal_02_s.dds";
    specularPower[1] = "1";
    useAnisotropic[1] = "1";
    translucent = "1";
    translucentZWrite = "1";
    alphaTest = "0";
    alphaRef = "255";
    castShadows = "0";
    specularStrength[0] = "0.0980392";
};


singleton Material(road_01_damage_sml_decal_01)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-01_damage_sml_decal_01_d.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    materialTag0 = "beamng";
    materialTag1 = "decal";
    materialTag2 = "RoadAndPath";
    specularPower[0] = "14";
    specularMap[0] = "road-01_damage_sml_decal_01_s.dds";
};


singleton Material(Road_01_tracks_large_decal_01)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "road-01_tracks_large_decal_01.dds";
    useAnisotropic[0] = "1";
    castShadows = "0";
    translucent = "1";
    translucentZWrite = "1";
    materialTag0 = "beamng";
    materialTag1 = "decal";
    materialTag2 = "RoadAndPath";
    specularPower[0] = "14";
};

singleton Material(Utah_dirt_road)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "Utah_dirt_road_d.dds";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    specularPower[0] = "1";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.992157 0.992157 0.992157 1";
    scrollSpeed[0] = "4.118";
    diffuseColor[0] = "0.909804 0.909804 0.909804 1";
    normalMap[0] = "Utah_dirt_road_n.dds";
    specularMap[0] = "Utah_dirt_road_s.dds";
};

singleton Material(Utah_dirt_trail)
{
    mapTo = "unmapped_mat";
    diffuseMap[0] = "Utah_dirt_trail_d.dds";
    materialTag0 = "RoadAndPath";
    materialTag1 = "beamng";
    specularPower[0] = "29";
    pixelSpecular[0] = "0";
    useAnisotropic[0] = "1";
    translucent = "1";
    translucentZWrite = "1";
    specular[0] = "0.992157 0.992157 0.992157 1";
    scrollSpeed[0] = "4.118";
    materialTag2 = "RoadAndPath";
    normalMap[0] = "Utah_dirt_trail_n.dds";
    specularMap[0] = "Utah_dirt_trail_s.dds";
};

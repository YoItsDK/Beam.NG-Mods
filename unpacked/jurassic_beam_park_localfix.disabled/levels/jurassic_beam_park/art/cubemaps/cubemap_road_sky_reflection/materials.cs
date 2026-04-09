singleton CubemapData( cubemap_road_sky_reflection )
{
    cubeFace[0] = "./cubemap/skybox_1";
    cubeFace[1] = "./cubemap/skybox_2";
    cubeFace[2] = "./cubemap/skybox_3";
    cubeFace[3] = "./cubemap/skybox_4";
    cubeFace[4] = "./cubemap/skybox_5";
    cubeFace[5] = "./cubemap/skybox_6";
};

singleton Material(road_sky_reflection)
{
    mapTo = "unmapped_mat";
    materialTag0 = "beamng";
    cubemap = "road_sky_reflection";
    materialTag1 = "Natural";
    materialTag2 = "BNG_sky";
};

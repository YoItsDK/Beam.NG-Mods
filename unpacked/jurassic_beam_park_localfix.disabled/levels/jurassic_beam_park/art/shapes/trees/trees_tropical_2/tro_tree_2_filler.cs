
singleton TSShapeConstructor(Tro_tree_2_fillerDae)
{
   baseShape = "./tro_tree_2_filler.dae";
};

function Tro_tree_2_fillerDae::onLoad(%this)
{
   %this.addImposter("25", "4", "0", "0", "256", "1", "0");
}

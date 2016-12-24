$Blood::DripTimeOnBlood = 5;
$Blood::CheckpointCount = 30;
$Blood::dryTime = 5 * 60000; //5 minutes
if ($Blood::doDamageCheck $= "")
	$Blood::doDamageCheck = true;

$Blood::DripOnDamage = 0;
$Blood::SprayOnDamage = 1;
$Blood::SprayOnDeath = 1;
$Blood::Effects = 1;

exec("./datablocks.cs");
exec("./decals.cs");
exec("./damage.cs");
exec("./support.cs");
exec("./blood.cs");

//Blood event
registerOutputEvent(Player, setBloody, "list ALL 0 chest 1 left_hand 2 right_hand 3 left_shoe 4 right_shoe 5" TAB "list ALL 0 front 1 back 2" TAB "bool 0", 1);
function Player::setBloody(%this, %type, %dir, %bool, %client)
{
	switch(%type)
	{
		case 1:
			if (%dir == 1)
				%this.bloody["chest_front"] = %bool;
			else if (%dir == 2)
				%this.bloody["chest_back"] = %bool;
			else
			{
				%this.bloody["chest_front"] = %bool;
				%this.bloody["chest_back"] = %bool;
			}
		case 2:
			%this.bloody["lhand"] = %bool;
		case 3:
			%this.bloody["rhand"] = %bool;
		case 4:
			%this.bloody["lshoe"] = %bool;
			if(!%bool)
				%this.bloodyFootprints = 0;
		case 5:
			%this.bloody["rshoe"] = %bool;
			if(!%bool)
				%this.bloodyFootprints = 0;
		default:
			%this.bloody["lshoe"] = %bool;
			%this.bloody["rshoe"] = %bool;
			%this.bloody["lhand"] = %bool;
			%this.bloody["rhand"] = %bool;
			%this.bloody["chest_front"] = %bool;
			%this.bloody["chest_back"] = %bool;
			%this.bloody["chest_lside"] = %bool;
			%this.bloody["chest_rside"] = %bool;
			if(!%bool)
				%this.bloodyFootprints = 0;
	}
	if (isObject(%client))
	{
		%client.applyBodyParts();
		%client.applyBodyColors();
	}
}
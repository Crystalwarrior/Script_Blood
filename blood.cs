
function BloodDripProjectile::onCollision(%this, %obj, %col, %pos, %fade, %normal) {
	if(!isObject(%col)) return;
	if (%col.getType() & $TypeMasks::FxBrickObjectType && !%obj.isPaint) {
		initContainerRadiusSearch(
			%pos, 0.5,
			$TypeMasks::ShapeBaseObjectType
		);

		while (isObject( %find = containerSearchNext())) {
			if (%find.isBlood) {
				%find.delete();
				break;
			}
		}
	}

	if (%col.getType() & $TypeMasks::PlayerObjectType && !%obj.isPaint) {
		%col.startDrippingBlood($Blood::DripTimeOnBlood);
	}
	%obj.explode();
}

function BloodDripProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal)
{
	if (%col.getType() & ($TypeMasks::FxBrickObjectType | $TypeMasks::TerrainObjectType))
	{
		initContainerRadiusSearch(%pos, 0.1,
			$TypeMasks::ShapeBaseObjectType);

		while (isObject(%col = containerSearchNext()))
		{
			if (!%col.isBlood || %col.getDataBlock() != pegprintDecal.getId())
				continue;
			%found = %col;
		}
		if (%found)
		{
			%decal = %found;
			%decal.alpha = getMin(%decal.alpha + 0.01, 1);
			%decal.setScale(vectorAdd(%decal.getScale(), "0.05 0.05 0.05"));//vectorMin(vectorAdd(%decal.getScale(), "0.05 0.05 0.05"),"5 5 5"));
			%decal.freshness += 0.05;
		}
		else
		{
			%decal = spawnDecal(pegprintDecal, %pos, %normal);
			%decal.setScale("1 1 1");
			%decal.setTransform(vectorAdd(%decal.getPosition(), "0 0 0.01"));
			%decal.alpha = 0.6;
			%decal.isBlood = true;
			%decal.sourceClient = %obj.client;
			%decal.spillTime = $Sim::Time;
			%decal.freshness = 0.5; //freshness < 1 means can't get bloody footprints from it
		}
		%decal.color = "0.7 0 0" SPC getMin(%decal.alpha, 1);
		%decal.setNodeColor("ALL", %decal.color);
	}
	%obj.explode();
	return;
}

function BloodDripProjectile::onExplode(%this, %obj, %pos)
{
	ServerPlay3D(bloodDripSound @ getRandom(1, 4), %pos);
}

function createBloodDripProjectile(%position, %size, %paint) {
	%obj = new Projectile() {
		dataBlock = BloodDripProjectile;

		initialPosition = %position;
		initialVelocity = "0 0 -2";
		isPaint = %paint;
	};

	MissionCleanup.add(%obj);

	if (%size !$= "") {
		%obj.setScale(%size SPC %size SPC %size);
	}

	return %obj;
}

function Player::startDrippingBlood(%this, %duration) {
	%duration = mClampF(%duration, 0, 60);
	%remaining = %this.dripBloodEndTime - $Sim::Time;

	if (%duration == 0 || (%this.dripBloodEndTime !$= "" && %duration < %remaining)) {
		return;
	}

	%this.dripBloodEndTime = $Sim::Time + %duration;

	if (!isEventPending(%this.dripBloodSchedule)) {
		%this.dripBloodSchedule = %this.schedule(getRandom(300, 800), dripBloodSchedule);
	}
}

function Player::stopDrippingBlood(%this) {
	%this.dripBloodEndTime = "";
	cancel(%this.dripBloodSchedule);
}

function Player::dripBloodSchedule(%this) {
	cancel(%this.dripBloodSchedule);

	if ($Sim::Time >= %this.dripBloodEndTime) {
		return;
	}

	%this.doDripBlood(true);
	%this.dripBloodSchedule = %this.schedule(getRandom(300, 800), dripBloodSchedule);
}

function Player::doDripBlood(%this, %force, %start, %end) {
	if (!%force && $Sim::Time - %this.lastBloodDrip <= 0.2) {
		return false;
	}
	if (%start $= "") {
		%start = vectorAdd(%this.position, "0 0 0.1");
	}
	if (%end $= "") {
		%end = vectorSub(%this.position, "0 0 0.1");
	}

	%ray = containerRayCast(%start, %end, $TypeMasks::FxBrickObjectType | $TypeMasks::TerrainObjectType);

	%this.lastBloodDrip = $Sim::Time;
	%decal = spawnDecalFromRay(%ray, BloodDecal @ getRandom(1, 2), 0.2 + getRandom() * 0.85);
	if (isObject(%decal)) {
		%decal.isBlood = true;
		%decal.color = "0.7 0 0 1";
		%decal.setNodeColor("ALL", %decal.color);
		%decal.sourceClient = %this.client;
		%decal.spillTime = $Sim::Time;
		%decal.freshness = 0.5;
		%decal.bloodDryingSchedule = schedule($Blood::dryTime, 0, bloodDryingLoop, %decal);
	}

	return true;

	%this.lastBloodDrip = $Sim::Time;

	%x = getRandom() * 6 - 3;
	%y = getRandom() * 6 - 3;
	%z = 0 - (20 + getRandom() * 40);

	return true;
}

function Player::doSplatterBlood(%this, %amount, %pos) {
	if (%pos $= "") {
		%pos = %this.getHackPosition();
	}

	if (%amount $= "") {
		%amount = getRandom(15, 30);
	}

	%masks = $TypeMasks::FxBrickObjectType | $TypeMasks::TerrainObjectType;
	%spread = 0.25 + getRandom() * 0.25;

	for (%i = 0; %i < %amount; %i++) {
		%cross = vectorScale(vectorSpread("0 0 -1", %spread), 6);
		%stop = vectorAdd(%pos, %cross);

		%ray = containerRayCast(%pos, %stop, %masks);
		%scale = 0.6 + getRandom() * 0.85;
		%decal = spawnDecalFromRay(%ray, BloodDecal @ getRandom(1, 2), %scale);
		if(getWord(%ray, 1))
		{
			%decal.isBlood = true;
			%decal.color = "0.7 0 0 1";
			%decal.setNodeColor("ALL", %decal.color);
			%decal.sourceClient = %this.client;
			%decal.spillTime = $Sim::Time;
			%decal.freshness = 3; //Basically amount of times someone can step in blood
			%decal.bloodDryingSchedule = schedule($Blood::dryTime, 0, bloodDryingLoop, %decal);
			// serverPlay3d(bloodSpillSound, getWords(%ray, 1, 3));
			createBloodExplosion(getWords(%ray, 1, 3), vectorNormalize(%this.getVelocity()), %scale SPC %scale SPC %scale);
			if(vectorDot("0 0 -1", %decal.normal) >= 0.5 && !isEventPending(%decal.ceilingBloodSchedule)) {
				if(getRandom(0, 3) == 3)
				{
					%decal.ceilingBloodSchedule = schedule(getRandom(16, 500), 0, ceilingBloodLoop, %decal, getWords(%ray, 1, 3));
				}
			}
		}
	}
}

function GameConnection::period(%this)
{
	messageAll('', '\c3%1 \c5is on their period.', %this.character.name);
	%this.player.period();
}

function Player::period(%this)
{
	cancel(%this.period);

	if (%this.getState() $= "Dead")
		return;

	%this.doSplatterBlood(1);
	%this.period = %this.schedule(25, "period");
}

function Player::doBloodyFootprint(%this, %ray, %foot, %alpha) {
	if(%alpha $= "")
		%alpha = 1;
	if(%alpha <= 0)
		return;
	%datablock = footprintDecal;
	if(isObject(%this.client))
	{
		if(%foot)
			%datablock = %this.client.lleg ? pegprintDecal : footprintDecal;
		else
			%datablock = %this.client.rleg ? pegprintDecal : footprintDecal;
	}
	%decal = spawnDecalFromRay(%ray, %datablock, 0.3);
	%decal.setScale("1 1 1");
	%set = vectorAdd(%decal.getTransform(), "0 0 0.05");
	%decal.setTransform(%set SPC getWords(%this.getTransform(), 3, 7));

	//%decal.setNodeColor("ALL", %obj.client.murderPantsColor);
	%decal.color = "0.7 0 0" SPC %alpha;
	%decal.setNodeColor("ALL", %decal.color);
	%decal.isBlood = true;
	if(isObject(%this.bloodClient))
		%decal.sourceClient = %this.bloodClient;
	else
		%decal.sourceClient = %this.client;
	%decal.spillTime = $Sim::Time;
	%decal.freshness = 0.5; //freshness < 1 means can't get bloody footprints from it
	%decal.bloodDryingSchedule = schedule($Blood::dryTime, 0, bloodDryingLoop, %decal);
	%decal.isFootprint = true;
}

function Player::setBloodyFootprints(%this, %val, %bloodclient)
{
	%this.bloodyFootprints = %val;
	%this.bloodyFootprintsLast = %val;
	%this.bloodClient = %bloodclient;
	%this.bloody["lshoe"] = true;
	%this.bloody["rshoe"] = true;
	if (%this.client)
	{
		%this.client.applyBodyParts();
		%this.client.applyBodyColors();
	}
}

function ceilingBloodLoop(%this, %pos, %paint) {
	cancel(%this.ceilingBloodSchedule);
	if(!isObject(%this))
	{
		return;
	}

	if(!%this.driptime) {
		%this.driptime = 500;
	}

	%this.driptime = %this.driptime + 10;
	if(%this.driptime > 3000) {
		return;
	}

	if(%pos $= "") {
		return;
	}

	createBloodDripProjectile(%pos, "", %paint);
	%this.ceilingBloodSchedule = schedule(%this.driptime, 0, ceilingBloodLoop, %this, %pos, %paint);
}

function bloodDryingLoop(%this) {
	cancel(%this.bloodDryingSchedule);
	if(!isObject(%this)) return;
	if(%this.freshness <= 0)
	{
		%this.color = vectorMax("0 0 0", vectorSub(getWords(%this.color, 0, 2), "0.1 0.1 0.1")) SPC getWord(%this.color, 3);
		%this.setNodeColor("ALL", %this.color); //Make it appear darker
		return;
	}
	%this.freshness--;
	%this.bloodDryingSchedule = schedule($Blood::dryTime, 0, bloodDryingLoop, %this);
}

function createBloodExplosion(%position, %velocity, %scale) {
	%datablock = bloodExplosionProjectile @ getRandom(1, 2);
	%obj = new Projectile() {
		dataBlock = %datablock;

		initialPosition = %position;
		initialVelocity = %velocity;
	};

	MissionCleanup.add(%obj);

	%obj.setScale(vectorMin(%scale, "1 1 1"));
	%obj.explode();
}

function createBloodSplatterExplosion(%position, %velocity, %scale) {
	%datablock = bloodExplosionProjectile3;
	%obj = new Projectile() {
		dataBlock = %datablock;

		initialPosition = %position;
		initialVelocity = %velocity;
	};

	MissionCleanup.add(%obj);

	%obj.setScale(vectorMin(%scale, "1 1 1"));
	%obj.explode();
}
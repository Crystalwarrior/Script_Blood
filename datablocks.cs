datablock AudioDescription( AudioSilent3d : AudioClose3d )
{
	maxDistance = 15;
	referenceDistange = 5;
};
datablock AudioProfile(BloodSpillSound) {
	fileName = "./blood_Spill.wav";
	description = AudioSilent3D;
	preload = true;
};

datablock AudioProfile(BloodDripSound1) {
	fileName = "./blood_drip1.wav";
	description = AudioSilent3D;
	preload = true;
};
datablock AudioProfile(BloodDripSound2) {
	fileName = "./blood_drip2.wav";
	description = AudioSilent3D;
	preload = true;
};
datablock AudioProfile(BloodDripSound3) {
	fileName = "./blood_drip3.wav";
	description = AudioSilent3D;
	preload = true;
};
datablock AudioProfile(BloodDripSound4) {
	fileName = "./blood_drip4.wav";
	description = AudioSilent3D;
	preload = true;
};

datablock StaticShapeData(footprintDecal)
{
	shapeFile = "./footprint.dts";
};

datablock StaticShapeData(pegprintDecal)
{
	shapeFile = "./pegprint.dts";
};

datablock staticShapeData(BloodDecal1) {
	shapeFile = "./blood1.dts";

	doColorShift = true;
	colorShiftColor = "0.7 0 0 1";
};

datablock staticShapeData(BloodDecal2) {
	shapeFile = "./blood2.dts";

	doColorShift = true;
	colorShiftColor = "0.7 0 0 1";
};

datablock ParticleData(bloodParticle)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.2;
	gravityCoefficient	= 0.2;
	inheritedVelFactor	= 0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 500;
	lifetimeVarianceMS	= 10;
	spinSpeed		= 40.0;
	spinRandomMin		= -50.0;
	spinRandomMax		= 50.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "./res/particles/blood2.png";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.7 0 0 1";
	colors[1]	= "0.7 0 0 0";
	sizes[0]	= 0.4;
	sizes[1]	= 2;
	//times[0]	= 0.5;
	//times[1]	= 0.5;
};

datablock ParticleEmitterData(bloodEmitter)
{
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;

	ejectionVelocity = 0; //0.25;
	velocityVariance = 0; //0.10;

	ejectionOffset = 0;

	thetaMin         = 0.0;
	thetaMax         = 90.0;

	particles = bloodParticle;

	useEmitterColors = true;
	uiName = "";
};

datablock ExplosionData(bloodExplosion)
{
	//explosionShape = "";
	//soundProfile = bulletHitSound;
	lifeTimeMS = 300;

	particleEmitter = bloodEmitter;
	particleDensity = 5;
	particleRadius = 0.2;
	//emitter[0] = bloodEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";
};

datablock ProjectileData(bloodExplosionProjectile1)
{
	directDamage        = 0;
	impactImpulse	     = 0;
	verticalImpulse	  = 0;
	explosion           = bloodExplosion;
	particleEmitter     = bloodEmitter;

	muzzleVelocity      = 50;
	velInheritFactor    = 1;

	armingDelay         = 0;
	lifetime            = 2000;
	fadeDelay           = 1000;
	bounceElasticity    = 0.5;
	bounceFriction      = 0.20;
	isBallistic         = true;
	gravityMod = 0.1;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};



datablock ParticleData(bloodParticle2)
{
	dragCoefficient		= 3.0;
	windCoefficient		= 0.1;
	gravityCoefficient	= 0.3;
	inheritedVelFactor	= 1;
	constantAcceleration	= 0.0;
	lifetimeMS		= 300;
	lifetimeVarianceMS	= 10;
	spinSpeed		= 20.0;
	spinRandomMin		= -10.0;
	spinRandomMax		= 10.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "./res/particles/blood3.png";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.7 0 0 1";
	colors[1]	= "0.7 0 0 0";
	sizes[0]	= 1;
	sizes[1]	= 0;
	//times[0]	= 0.5;
	//times[1]	= 0.5;
};

datablock ParticleEmitterData(bloodEmitter2)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 0; //0.25;
	velocityVariance = 0; //0.10;

	ejectionOffset = 0;

	thetaMin         = 0.0;
	thetaMax         = 90.0;

	particles = bloodParticle2;

	useEmitterColors = true;
	uiName = "";
};

datablock ExplosionData(bloodExplosion2)
{
	//explosionShape = "";
	//soundProfile = bulletHitSound;
	lifeTimeMS = 300;

	particleEmitter = bloodEmitter2;
	particleDensity = 5;
	particleRadius = 0.2;
	//emitter[0] = bloodEmitter;

	faceViewer     = true;
	explosionScale = "1 1 1";
};

datablock ProjectileData(bloodExplosionProjectile2)
{
	directDamage        = 0;
	impactImpulse	     = 0;
	verticalImpulse	  = 0;
	explosion           = bloodExplosion2;
	particleEmitter     = bloodEmitter2;

	muzzleVelocity      = 50;
	velInheritFactor    = 1;

	armingDelay         = 0;
	lifetime            = 2000;
	fadeDelay           = 1000;
	bounceElasticity    = 0.5;
	bounceFriction      = 0.20;
	isBallistic         = true;
	gravityMod = 0.1;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

datablock ParticleEmitterData(bloodEmitter3)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;

	ejectionVelocity = 8;
	velocityVariance = 2;
	orientParticles = 0;
	ejectionOffset = 0.2;

	thetaMin         = 0;
	thetaMax         = 25;

	particles = bloodParticle;

	useEmitterColors = true;
	uiName = "";
};

datablock ExplosionData(bloodExplosion3)
{
	//explosionShape = "";`
	//soundProfile = bulletHitSound;
	lifeTimeMS = 65;

	particleEmitter = "";
	particleDensity = 0.2;
	particleRadius = 30;
	emitter[0] = "bloodEmitter3";

	faceViewer     = true;
	explosionScale = "1 1 1";
};

datablock ProjectileData(bloodExplosionProjectile3)
{
	directDamage        = 0;
	impactImpulse	     = 0;
	verticalImpulse	  = 0;
	explosion           = bloodExplosion3;

	muzzleVelocity      = 50;
	velInheritFactor    = 1;

	armingDelay         = 0;
	lifetime            = 2000;
	fadeDelay           = 1000;
	bounceElasticity    = 0.5;
	bounceFriction      = 0.20;
	isBallistic         = true;
	gravityMod = 0.1;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

datablock ParticleData(bloodDripParticle)
{
	dragCoefficient		= 1.0;
	windCoefficient		= 0.1;
	gravityCoefficient	= 0.5;
	inheritedVelFactor	= 1;
	constantAcceleration	= 0.0;
	lifetimeMS		= 200;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 20.0;
	spinRandomMin		= -10.0;
	spinRandomMax		= 10.0;
	useInvAlpha		= true;
	animateTexture		= false;
	//framesPerSec		= 1;

	textureName		= "base/data/particles/dot.png";
	//animTexName		= " ";

	// Interpolation variables
	colors[0]	= "0.7 0 0 1";
	colors[1]	= "0.7 0 0 0.8";
	sizes[0]	= 0.1;
	sizes[1]	= 0;
	//times[0]	= 0.5;
	//times[1]	= 0.5;
};

datablock ParticleEmitterData(bloodDripEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0; //0.25;
	velocityVariance = 0; //0.10;

	ejectionOffset = 0;

	thetaMin         = 0.0;
	thetaMax         = 90.0;

	particles = bloodDripParticle;

	useEmitterColors = true;
	uiName = "";
};

datablock ProjectileData(BloodDripProjectile)
{
	directDamage        = 0;
	impactImpulse	     = 0;
	verticalImpulse	  = 0;
	explosion           = bloodExplosion2;
	particleEmitter     = bloodDripEmitter;

	muzzleVelocity      = 60;
	velInheritFactor    = 1;

	armingDelay         = 3000;
	lifetime            = 3000;
	fadeDelay           = 2000;
	bounceElasticity    = 0.5;
	bounceFriction      = 0.20;
	isBallistic         = true;
	gravityMod = 1;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};

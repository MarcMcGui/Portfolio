class_name Unit extends CharacterBody3D

@export var gameManager : GameManager
@export var team : int
@export var speed : float
@export var acceleration: float = 100 
@export var target : Vector3 = Vector3.ZERO ##current point to path toward
@export var health : int = 10 
@export var damage : int = 2
@export var visionSphere : Area3D ##cone of vision
@export var body : CSGBox3D
@export var attackRange : Area3D
@export var range : float = 1
@export var jumpHeight: float = 0.5 
@export var cameraSensativity: float = 0.003
@export var camera: Camera3D
@export var knockBack : float = 20
@export var attackCooldown : Timer
@export var attackCoolDowntime : float
@export var hitDelay : Timer
@export var color : Color
@export var attackDelay : float = 0
@export var deathParticle : GPUParticles3D
@export var hitParticle : GPUParticles3D
@export var Weapon : Node3D
@export var healthBar : ProgressBar
@export var PossessedUI : CanvasLayer
@export var playerMaterial : StandardMaterial3D = preload("res://Material/unit.tres")
@export var enemyMaterial : StandardMaterial3D = preload("res://Material/enemy unit.tres")
@export var unitType : String

var rays
var possessedDamage : float
var possessedHealth : float
var possessedTotalHealth : float
var unitDamage : float
var unitHealth : float
var unitTotalHealth : float
var totalHealth : float
var baseDamage : float
var currentEnemy : Unit
var enemyInSight = false
var hasTarget : bool = false
var EnemyInSight : bool = false
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
var gravVel: Vector3
var hitback : Vector3 = Vector3.ZERO
var Possesed = false
var jumping = false
var mouseLocked = false
var walkDir: Vector2
var lookDir: Vector2
var walkVel: Vector3
var jumpVel: Vector3
var pushBack : bool = false
var hitDir : Vector3 = Vector3.ZERO
var hitForce : float = 0
var beingHit : bool = false
var incomingDamage : float = 0
var isAttacking : bool = false


func _ready():
	baseDamage = damage
	rays = find_child("Rays").get_children()
	PossessedUI.visible = false
	totalHealth = health
	possessedDamage = damage * 4
	possessedHealth = health * 10
	possessedTotalHealth = totalHealth * 10
	var collideWithFloor = get_collision_layer_value(1)
	var maskFloor = get_collision_mask_value(1)
	gameManager = get_parent().find_child("GameManager")
	pass

func _gravity(delta):
	gravVel = Vector3.ZERO if is_on_floor() else gravVel.move_toward(Vector3(0, velocity.y - gravity, 0), gravity * delta)
	return gravVel

# Called every frame. 'delta' is the elapsed time since the previous frame.

		
func _ai_move(delta) :
	var tar = target
	var objectsInSight = visionSphere.get_overlapping_bodies()
	if objectsInSight.size() > 0:
		if currentEnemy == null:
			for obj in objectsInSight:
				if obj is Unit:
					var unit : Unit = obj
					var unitTeam = unit.team
					if unitTeam != team: 
						if currentEnemy != null:
							if global_position.distance_to(unit.global_position) < global_position.distance_to(currentEnemy.global_position):
								currentEnemy = unit
						else :
							currentEnemy= unit
						enemyInSight = true
						if self is PlayerSoldier && (unit is Rogue):
							enemyInSight = false
							currentEnemy = null

	if enemyInSight :
		if currentEnemy != null:
			tar = currentEnemy.position
			hasTarget = true
		else :
			enemyInSight = false
	if hasTarget:
		var direct = calcTarget(tar)
		var lookatlocation = tar
		lookatlocation.y = global_transform.origin.y
		look_at(lookatlocation, Vector3.UP)
		if !enemyInSight || !isEnemyInRange():
			pathAroundBlockers(lookatlocation, delta, direct)
		if isEnemyInRange():
			attackStart()
			velocity = _gravity(delta) + _pushBack(delta)
	else:
		velocity = _gravity(delta) + _pushBack(delta)
	move_and_slide()

func pathAroundBlockers(pos, delta, direct) :
#	var spaceState = get_world_3d().direct_space_state
#	var params = PhysicsRayQueryParameters3D.new()
#	params.from = transform.origin
#	params.to = pos
#	params.exclude = []
#	var rayArray = spaceState.intersect_ray(params)
	if global_position.distance_to(pos) > 0.1:
		velocity = _gravity(delta) + (getBestDir(direct, pos) * speed) + _pushBack(delta)
	else:
		velocity = _gravity(delta) + _pushBack(delta)
#	if rayArray.has("position") :
#		var distanceToBlocker = abs(global_position - rayArray["position"])
#		if distanceToBlocker.z < 0.3 && distanceToBlocker.x < 0.3:
#			velocity = _gravity(delta) + (getBestDir(direct)*speed) + _pushBack(delta)

func getBestDir(direct : Vector3, pos : Vector3) :
	var bestDir = Vector3.ZERO
	var bestAmp = -1
	var dir = Vector2(direct.x, direct.z)
	for ray in rays:
		var curRay : RayCast3D = ray
		var rayPos = ((curRay.target_position)).normalized()
		var curAngle = Vector3(rayPos.x, 0, rayPos.y)
		var amp = dir.dot(Vector2(rayPos.x, rayPos.y))
		var colliding = curRay.is_colliding()
		if colliding == false:
			if amp >= bestAmp:
				bestDir = curAngle
				bestAmp = amp
	return direct + (bestDir).normalized()

func calcTarget(tar : Vector3):
	var distance = abs(global_position - tar)
	var direct = global_position.direction_to(tar)
	return Vector3(direct.x, 0, direct.z)

func isHit(hitBy : Unit) :

	incomingDamage = hitBy.damage
	hitDir = -(global_position.direction_to(hitBy.global_position))
	hitForce = hitBy.knockBack
	pushBack = true
	var clas = ""
	match unitType:
		"Rogue" :
			if hitBy is PlayerSoldier:
				incomingDamage = incomingDamage/3
			elif hitBy is Wizard:
				incomingDamage = incomingDamage * 3
		"Wizard" :
			if hitBy is Rogue:
				incomingDamage = incomingDamage/3
			elif hitBy is PlayerSoldier:
				incomingDamage = incomingDamage * 3
		"PlayerSoldier" :
			if hitBy is Wizard:
				incomingDamage = incomingDamage/5
			elif hitBy is Rogue:
				incomingDamage = incomingDamage * 3
		_:
			incomingDamage = incomingDamage
	health -= incomingDamage
	healthBar.value = (health / totalHealth) * 100
	PossessedUI.find_child("ProgressBar").value = healthBar.value
	hitParticle.restart()

func changeMaterial() :
	if team == 1 :
		set_collision_layer_value(2, true)
		set_collision_layer_value(5, false)
		set_collision_mask_value(5, true)
		set_collision_mask_value(2, false)
		body.material_override = playerMaterial
	else :
		set_collision_layer_value(5, true)
		set_collision_layer_value(2, false)
		set_collision_mask_value(2, true)
		set_collision_mask_value(5, false)
		body.material_override = enemyMaterial

func _input(event):
	if (Input.is_action_just_released("Left Click")) :
		if Possesed:
			hitDelay.start(attackDelay)
			attackStart()
		elif !gameManager.directControl:
			if screen_to_ray():
				if team == 1:
					possess()
	if Input.is_action_just_pressed("Exit") :
		if Possesed:
			unPossess()

func isEnemyInRange():
	if attackRange.get_overlapping_bodies().size() > 0:
		var unitsInRange = attackRange.get_overlapping_bodies()
		for obj in unitsInRange:
			if obj is Unit:
				if obj == currentEnemy:
					return true
			elif obj.name.contains("SpawnTower"):
				if obj.team != team:
					return true
	return false

func isTowerInRange() :
	if attackRange.get_overlapping_bodies().size() > 0:
		var towersinRange = attackRange.get_overlapping_bodies()
		for obj in towersinRange:
			if obj is SpawnTower:
				if !enemyInSight :
					return true
	return false

func _unhandled_input(event: InputEvent):
	if Possesed:
		if event is InputEventMouseMotion and Input.mouse_mode == Input.MOUSE_MODE_CAPTURED:
			rotate_y(-event.relative.x * cameraSensativity)
			camera.rotate_x(-event.relative.y * cameraSensativity)
			camera.rotation.x = clampf($Camera3D.rotation.x, -deg_to_rad(70), deg_to_rad(70))
		if Input.is_action_just_pressed("jump"): jumping = true

func _physics_process(delta: float) -> void:
	if team == 1:
		hitParticle.material_override = playerMaterial
		deathParticle.material_override = playerMaterial
		body.material = playerMaterial
	else:
		deathParticle.material_override = enemyMaterial
		hitParticle.material_override = enemyMaterial
		body.material = enemyMaterial
	set_axis_lock(PhysicsServer3D.BODY_AXIS_ANGULAR_X, true)
	set_axis_lock(PhysicsServer3D.BODY_AXIS_ANGULAR_Z, true)
	
	if hitDelay.time_left <= 0 && isAttacking:
		isAttacking = false
		attack()
	
	if(!Possesed):
		_ai_move(delta)
	else :
		_player_Move(delta)
	if(health < 1):
		onDelete()


func _walk(delta):
	walkDir = Input.get_vector("move_left", "move_right", "move_forward", "move_back")
	var _forward = camera.global_transform.basis * Vector3(walkDir.x, 0, walkDir.y)
	var walk_dir = Vector3(_forward.x, 0, _forward.z).normalized()
	walkVel = walkVel.move_toward(walk_dir * speed * walkDir.length(), acceleration * delta)
	return walkVel

func _pushBack(delta):
	var push_vel : Vector3 = Vector3.ZERO
	if pushBack :
		if is_on_floor(): 
			push_vel = Vector3((hitForce * hitDir.x), (jumpHeight) * gravity, (hitForce*hitDir.z))
		pushBack = false
		return push_vel
	push_vel = Vector3.ZERO if is_on_floor() else push_vel.move_toward(Vector3.ZERO, gravity * delta)
	return push_vel

func _jump(delta):
	if jumping:
		if is_on_floor(): 
			jumpVel = Vector3(0, jumpHeight * gravity, 0)
		jumping = false
		return jumpVel
	jumpVel = Vector3.ZERO if is_on_floor() else jumpVel.move_toward(Vector3.ZERO, gravity * delta)
	return jumpVel
	pass

		
func attack() :
	pass
func attackStart():
	pass

func _player_Move(delta) :
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	mouseLocked = true
	velocity = _walk(delta) + _gravity(delta) + _jump(delta) + _pushBack(delta)
	move_and_slide()
	pass

func onDelete() :
	if Possesed:
		unPossess()

	deathParticle.emitting = true
	set_process(false)
	set_physics_process(false)
	for child in get_children():
		if !(child is GPUParticles3D) :
			child.queue_free()
	await get_tree().create_timer(1).timeout
	queue_free()

func screen_to_ray():
	var spaceState = get_world_3d().direct_space_state
	var mouse = get_viewport().get_mouse_position()
	var camera = get_tree().root.get_camera_3d()
	var params = PhysicsRayQueryParameters3D.new()
	params.from = camera.project_ray_origin(mouse)
	params.to = params.from + camera.project_ray_normal(mouse) * 2000
	params.exclude = []
	var rayArray = spaceState.intersect_ray(params)
	if rayArray.has("rid"):
		var myRID  = get_rid();
		var raycastRID = rayArray["rid"]
		if rayArray["rid"] == get_rid():
			print("clicked on me!")
			return true
	return false
	
	pass

func possess() : 
	gameManager.currentViewport = camera.get_viewport()
	damage = possessedDamage
	health = possessedHealth
	totalHealth = possessedTotalHealth
	PossessedUI.visible = true
	healthBar.visible = false
	set_collision_layer_value(2, false)
	set_collision_layer_value(3, true)
	set_collision_mask_value(2, false)
	gameManager.directControl = true
	camera.make_current()
	Possesed = true
	body.visible = false
	
func unPossess() :
	
	PossessedUI.visible = false
	healthBar.visible = true
	Possesed = false
	set_collision_layer_value(2, true)
	set_collision_layer_value(3, false)
	set_collision_mask_value(2, true)
	gameManager.directControl = false
	var globalCamera : Camera3D = get_parent().find_child("Camera3D")
	camera.clear_current()
	globalCamera.make_current()
	mouseLocked = false
	Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
	body.visible = true
	

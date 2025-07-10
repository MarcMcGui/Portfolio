class_name SpawnTower 
extends StaticBody3D
var maxUnits = 5
var units = []
var timer = 100
var spawning = false
var waypoint
var soldier = preload("res://Objects/Soldier.tscn")
var wizard = preload("res://Objects/Wizard.tscn")
var rogue = preload("res://Objects/rogue.tscn")
var GameMange : GameManager
var bestSpwanChoice = preload("res://Objects/Soldier.tscn")
var playerTower
var unitsInPlay
var maxHealth

@export var spawnLocation : Area3D
@export var team : int 
@export var spawnFlag : Node3D
@export var playerMaterial : StandardMaterial3D = preload("res://Material/unit.tres")
@export var enemyMaterial : StandardMaterial3D = preload("res://Material/unit.tres")
@export var health : float
@export var healthBar : ProgressBar
@export var deathPart : GPUParticles3D


# Called when the node enters the scene tree for the first time.
func _ready():
	spawnLocation = $SpawnLocation
	maxHealth = health
	GameMange = get_parent().find_child("GameManager")
	var scene = get_parent().get_children()
	for child in scene:
		if child is SpawnTower :
			if child.team == 1 :
				playerTower = child
	pass # Replace with function body.
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta) :
	pass

func isMyUnit(a) -> bool:
	if a is Unit:
		if a.team == team:
			return true
	return false

func _physics_process(delta):
	if team == 1:
		player_tower()
	else :
		enemy_tower()
	var unitsNumber = 0
	unitsInPlay = get_parent().get_children().filter(isMyUnit)
	
	if (spawning):
		if (unitsInPlay.size() < maxUnits) :
			timer -= 1
			if timer < 1 :
				if team == 1:
					timer = 100
				else:
					timer = 500
				if playerTower.spawning || team == 1:

					StartSpawn()
	pass

func CheckSpawnArea():
	var loc = spawnLocation
	spawnLocation = $SpawnLocation
	print(spawnLocation)
	var unitsInSpawn = spawnLocation.get_overlapping_bodies()
	for bodies in unitsInSpawn:
		if bodies is Unit:
			if bodies.team == team :
				
				return true
	return false

func StartSpawn():
	spawning = true
	var sold : Unit
	if team == 1:
		match GameMange.unit_selected:
			"soldier" : sold = soldier.instantiate()
			"wizard" : sold = wizard.instantiate()
			"rogue" : sold = rogue.instantiate()
			_ : sold = soldier.instantiate()
	else :
		determine_spawn()
		sold = bestSpwanChoice.instantiate()
	sold.position = to_global(spawnLocation.position)
	sold.target = waypoint
	sold.hasTarget = true
	sold.team = team
	sold.changeMaterial()
	print(sold.target)
	get_parent().add_child(sold)

func update_playunits_waypoint(playerUnits) :
	for unit in playerUnits:
		if waypoint != null :
			unit.target = waypoint

func screen_to_ray():
	var spaceState = get_world_3d().direct_space_state
	var mouse = get_viewport().get_mouse_position()
	var camera = get_viewport().get_camera_3d()
	var params = PhysicsRayQueryParameters3D.new()
	if GameMange.directControl:
		params.from = camera.project_ray_origin(get_viewport().size/2)
		params.to = params.from + camera.project_ray_normal(get_viewport().size/2) * 2000
	else :
		params.from = camera.project_ray_origin(mouse)
		params.to = params.from + camera.project_ray_normal(mouse) * 2000
	params.exclude = []
	var rayArray = spaceState.intersect_ray(params)
	if rayArray.has("position"):
		return rayArray["position"]
	return Vector3(0,0,0)
	
	pass
	

func player_tower():
	if Input.is_action_pressed("Right Click"):
		waypoint = screen_to_ray()
		waypoint = Vector3(waypoint.x, 0.1, waypoint.z)
		for unit in unitsInPlay:
			if unit != null:
				unit.target = waypoint
		spawnFlag.position = to_local(waypoint)
		spawning = true
		

func enemy_tower() :
	if playerTower != null :
		waypoint = playerTower.global_position
	spawning = true

					
func determine_spawn() :
	var unitCount = { "soldiers" : 0, "wizards" : 0, "rogues" :0}
	
	for uni in get_parent().get_children() :
		if uni is Unit :
			if uni.team == 1:
				if uni is PlayerSoldier:
					unitCount["soldiers"] += 1
				if uni is Wizard:
					unitCount["wizards"] += 1
				if uni is Rogue:
					unitCount["rogues"] += 1
	var mostUnits = get_largest(unitCount)
	match mostUnits:
		"soldiers" : bestSpwanChoice = rogue
		"wizards" : bestSpwanChoice = soldier
		"rogues" : bestSpwanChoice = wizard
	
func get_largest(unitCount) :
	var max = 0
	var unitKey = "soldiers"
	for key in unitCount :
		var num = unitCount[key]
		if num > max:
			max = num
			unitKey = key
	return unitKey

func isHit(unit : Unit) :
	health -= unit.damage
	healthBar.value = (health / maxHealth) * 100
	if health < 1:
		set_process(false)
		set_physics_process(false)
		deathPart.emitting = true
		await get_tree().create_timer(1).timeout
		if team == 1:
			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
			get_tree().change_scene_to_file("res://Scenes/Lose Menu.tscn")
		else: 
			GameMange.checkingForWin = true
			GameMange.towers -= 1
		queue_free()
	

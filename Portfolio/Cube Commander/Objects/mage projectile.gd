class_name mageProjectile extends CharacterBody3D
@export var speed : float
@export var lifeTime : Timer
@export var hitZone : Area3D
@export var destroyParticle : GPUParticles3D
@export var trail : GPUParticles3D

var targetEnemy : Unit
var team : float = 0
var creator : Wizard
var direction : Vector3 = transform.basis.z

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if lifeTime.time_left < 1 :
		onDelete()
	pass
	
func _physics_process(delta):
	if creator == null:
		onDelete()
	elif creator.Possesed:
		velocity = direction * speed
		look_at(direction.normalized())
		
	
	else: 
		velocity = direction * speed
		
		look_at(direction.normalized())
	var hits = hitZone.get_overlapping_bodies()
	if hits.size() > 0:
		for hit  in hits:
			if hit is Unit:
				var unit : Unit = hit
				if unit.team != team:
					if unit.currentEnemy == null:
						unit.currentEnemy = creator
						unit.enemyInSight = true
					unit.isHit(creator)
					onDelete()
			elif hit is SpawnTower:
				if hit.team != creator.team:
					hit.isHit(creator)
					onDelete()
				
	move_and_slide()
	pass
	
func onDelete():
	destroyParticle.restart()
	set_process(false)
	set_physics_process(false)
	trail.queue_free()
	for child in get_children():
		if !(child is GPUParticles3D):
			child.queue_free()
	await get_tree().create_timer(1).timeout
	queue_free()
	pass


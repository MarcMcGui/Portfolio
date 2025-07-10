class_name Wizard extends Unit

@export var wandTip : CollisionShape3D

var spell = preload("res://Objects/mage projectule.tscn")
var currentSpell : mageProjectile
var canAttack : bool = false

var attack_dir : Vector3 = Vector3.ZERO

func attackStart() :
	if attackCooldown.time_left < 1:
		attack()
		attackCooldown.start(attackCoolDowntime)
	pass

func attack() :
	var projectileDirection
	currentSpell = spell.instantiate()
	currentSpell.creator = self
	currentSpell.position = wandTip.global_position
	currentSpell.set_collision_mask_value(2, get_collision_mask_value(2))
	currentSpell.set_collision_mask_value(5, get_collision_mask_value(5))
	projectileDirection = -camera.global_transform.basis.z 
	if Possesed:
		projectileDirection = -camera.global_transform.basis.z 

	else :
		if currentEnemy != null :
			if currentEnemy.body != null:
				projectileDirection = global_position.direction_to(currentEnemy.body.global_position)
		else:
			for obj in attackRange.get_overlapping_bodies():
				if obj.name.contains("SpawnTower"):
					projectileDirection = global_position.direction_to(obj.global_position)
			
	currentSpell.team = team
	currentSpell.direction = projectileDirection
	
	get_parent().add_child(currentSpell)
	

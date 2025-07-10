class_name Rogue extends Unit

@export var animTree : AnimationTree

func _process(delta):
	if attackCooldown.time_left <= 0:
		animTree["parameters/conditions/is_attacking"] = false
	pass

func attackStart() :
	if attackCooldown.time_left <= 0:
		isAttacking = true
		attackCooldown.start(attackCoolDowntime)
		animTree["parameters/conditions/is_attacking"] = true


func attack() :
	if attackRange.get_overlapping_bodies().size() > 0:
		var unitsInRange = attackRange.get_overlapping_bodies()
		for obj in unitsInRange:
			if obj is Unit || obj is SpawnTower:
				var attacking = obj
				var attackingTeam = attacking.team
				if (attackingTeam != team):
					var originalDamage = damage
					obj.isHit(self)

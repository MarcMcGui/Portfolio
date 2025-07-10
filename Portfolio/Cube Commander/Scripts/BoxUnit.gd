extends RigidBody3D
@export var target = Vector3(0,0,0)
@export var target2 = Vector3(0,0,0)
@export var thing = 555
var Possesed = false
var enemyInSight

func _physics_process(delta):
	if(!Possesed) :
		move_toward(target.x, target.y, target.z)
	
func _AI_Move() :
	pass
	
func _player_move() :
	pass
	

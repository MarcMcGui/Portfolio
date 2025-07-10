class_name GameManager extends Node3D

var directControl = false

var unit_selected = "soldier"
var checkingForWin
var towers = 3
var currentViewport : Viewport

@export var playerSpawnTower : SpawnTower

@export var UI : CanvasLayer
@export var wizardButton : CheckBox
@export var rogueButton : CheckBox
@export var soldierButton : CheckBox

func _ready():
	for tower in get_tree().root.find_children("SpawnTower") :
		if tower.team == 1:
			playerSpawnTower = tower
	checkingForWin = false

func _process(delta):
	if !isEnemyAlive() && (towers < 1):
		Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		get_tree().change_scene_to_file("res://Scenes/Win Menu.tscn")
	pass
			

func isEnemyAlive() :
	for child in get_parent().get_children(true):
		if child is SpawnTower:
			return (child.team == 2)
	return false

func _on_wizard_toggled(button_pressed):

	pass # Replace with function body.


func _on_rogue_pressed():
	print("rogue clicked")
	if unit_selected != "rogue":
		unit_selected = "rogue"
		rogueButton.disabled = true

		soldierButton.disabled = false
		wizardButton.disabled = false
		soldierButton.button_pressed = false
		wizardButton.button_pressed = false 
		
	pass # Replace with function body.


func _on_soldier_pressed():
	print("soldier clicked")
	if unit_selected != "soldier":
		unit_selected = "soldier"
		soldierButton.disabled = true
		
		wizardButton.disabled = false
		rogueButton.disabled = false
		wizardButton.button_pressed = false
		rogueButton.button_pressed = false 
		print(unit_selected)
	pass # Replace with function body.


func _on_wizard_2_pressed():
	print("wizard clicked")
	if unit_selected != "wizard":
		unit_selected = "wizard"
		wizardButton.disabled = true
		soldierButton.disabled = false
		rogueButton.disabled = false
		soldierButton.button_pressed = false
		rogueButton.button_pressed = false 
		print(unit_selected)
	pass # Replace with function body.

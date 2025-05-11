class_name Character
extends CharacterBody2D

var _direction: Vector2 = Vector2.ZERO

func _physics_process(delta: float) -> void:
    _direction = Vector2.ZERO
    if (Input.is_action_pressed("up")):
        _direction.y = -1.0
    if (Input.is_action_pressed("down")):
        _direction.y = 1.0
    if (Input.is_action_pressed("left")):
        _direction.x = -1.0
    if (Input.is_action_pressed("right")):
        _direction.x = 1.0
    velocity = _direction.normalized() * 200
    move_and_slide()

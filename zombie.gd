class_name Zombie
extends CharacterBody2D

@export var _target: CharacterBody2D
var _direction: Vector2 = Vector2.ZERO

func _physics_process(delta: float) -> void:
    _direction = _target.global_position - global_position
    velocity = _direction.normalized() * 150
    move_and_slide()

shader_type canvas_item;

uniform float amount: hint_range(0.0, 1.0, 0.1);
uniform vec3 color: source_color = vec3(1);

void fragment() {
	float current_a = texture(TEXTURE, UV).a;
	vec4 target_color = mix(texture(TEXTURE, UV), vec4(color, current_a), amount);
	COLOR = target_color;
}

void vertex() {
	
}

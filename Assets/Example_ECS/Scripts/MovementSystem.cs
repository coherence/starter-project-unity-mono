using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[AlwaysUpdateSystem]
class MovementSystem : SystemBase
{
    const float speed = 5.0f;

    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, in Input input) =>
        {
            var movement = new float3(input.Value.x * speed * dt,
                                      0f,
                                      input.Value.y * speed * dt);
            translation.Value = translation.Value + movement;
        }).ScheduleParallel();
    }
}

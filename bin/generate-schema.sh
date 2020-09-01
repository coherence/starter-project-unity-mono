mkdir ../Assets/coherence/generated

../coherence-bin/coherence-protocol-code-generator generate --code csharp --ecs unity --schema ../Assets/coherence/coherence.schema > ../Assets/coherence/generated/schema.cs

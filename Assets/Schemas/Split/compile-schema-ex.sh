#!/bin/sh

coherence-protocol-code-generator generate --code csharp --ecs unity --schema=../Combined.schema --split=true --sync=../Gathered.json --log-level=DEBUG

#!/bin/sh

echo "Removing all .cs and .cs.meta files in directory..."
find . -name "*.cs" -type f -delete
find . -name "*.cs.meta" -type f -delete

echo "Running protocol code generator (NOT generating Sync components)..."
coherence-protocol-code-generator generate --code csharp --ecs unity --schema=../Combined.schema --split=true

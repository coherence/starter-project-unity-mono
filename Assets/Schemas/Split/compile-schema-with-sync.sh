#!/bin/sh

echo "Removing Generic.cs (will clash)"
if [ -f ../Generic.cs ]; then
    rm ../Generic.cs
    rm ../Generic.cs.meta
fi

echo "Removing all .cs and .cs.meta files in directory..."
find . -name "*.cs" -type f -delete
find . -name "*.cs.meta" -type f -delete

echo "Concatenating Generics.schema with Gathered.schema to create Combined.schema..."
cat ../Generic.schema ../Gathered.schema > ../Combined.schema

echo "Running protocol code generator... (generating Sync components)"
coherence-protocol-code-generator generate --code csharp --ecs unity --schema=../Combined.schema --split=true --sync=../Gathered.json --log-level=DEBUG

## Scripts usage
1. Open powershell in project root folder.

2. Add migration with name "SomeMigrationName".
`.\migrations.ps1 add SomeMigrationName`

3. Remove the last migration (not applied to database).
`.\migrations.ps1 remove`

4. Update database based on migrations.
`.\updateDb.ps1`

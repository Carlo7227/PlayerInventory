rm diablo.sqlite
ls -a *.txt | sort |  awk  'BEGIN{print ".mode tabs"} {split($0,a,"."); print ".import " a[1] ".txt " a[1]}' > import_all_tables.sql
./sqlite3 diablo.sqlite < import_all_tables.sql

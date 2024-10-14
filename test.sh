#!/usr/bin/env bash

# regex to validate commit msg
commit_regex='(test|TEST)+(-)(\d)+\s+(#)(InProgress|Resolved|Done|Blocked|UAT)+\s+(Add|Cut|Fix|Bump|Make|Start|Stop|Refactor|Reformat|Optimize|Document|Merge)\s+(([a-zA-Z0-9]+)(\s|_)*)+'
error_msg="Commit message format is incorrect. Example: 'dmax-123 #InProgress Add Some changes'"

echo "#!/usr/bin/env bash
commit_regex='$commit_regex'
error_msg=\"$error_msg\"
if ! grep -qE \"\$commit_regex\" \"\$1\"; then
    echo \"\$error_msg\" >&2
    exit 1
fi
" > .githooks/commit-msg1

chmod +x .githooks/commit-msg1

echo "Completed git hook commit message setup"
# Workplace rules
This workspace is a small training sandbox for ticket investigation.

* Tickets are in `investigation/<KEY>.md`. Logs are in `investigation/attachments_<KEY>/` and are read-only.
* Sample product code is in `source/`. Product rules are in `docs/`. Treat both as read-only.
* Write new files only inside `investigation`, named `<KEY>_RCA.md` and `<KEY>_jira_comment.md`.
* Never post to Jira or any extrernal system. Never run terminal commands.
* Back every claim with evidence: a log line (with time) or a file and line number.
---
name: jira-comment
description: Formats an investigation result as a short, consistent Jira comment. Use when asked to write, draft or format a Jira comment from an RCA report
---

# Jira comment format

## When to use

After an RCA report exists in `investigation/<key>_RCA.md` and a Jira comment draft is needed.

## Steps

1. Read `investigation/<KEY>_RCA.md`.
2. Write the comment to `investigation/<KEY>_jira_comment.md` using the format below.

## Format

*Summary:* one sentence
*Root cause:* file and line
*Evidence:* two bullets, each quoting one log line
*Suggested fix:* one or two lines
*Affected version:* version from the ticket
*Owner team:* the team that owns the component
*Confidence:* High, Medium or Low

## Rules
* Maximum 80 words.
* Plain text only.
* Do not claim more certainity than the evidence supports.
* End with one question for the reporter. If nothing is missing, write "No furter info needed".
---
name: jira-comment
description: Formats an investigation result as a short, consistent Jira comment. Use when asked to write, draft or format a Jira comment from an RCA report. 
---

# Jira comment format

## When to use

After an RCA report exists in `investiation/<KEY>_RCA.md` and a Jira comment draft is needed.

## Steps

1. Read `investigation/<KEY>_RCA.md`.
2. write the comment to `investigation/<KEY>_jira_comment.md` using the format below.

## Format

TODO 1: change these headings to match how your team writes comments.

*Summary:* one sentence
*Root cause:* file and line
*Evidence:* two bullets, each quoting one log line
*Suggested fix:* one or tow lines
*Confidence:* Hgh, Medium or Low

## Rules

* Maximum 100 words. (TODO 2: change the limit if you like.)
* Plain  text only.
* Do not claim more certainity than the evidence supports.
* TODO 3: add one rule of your own, for example "end with one question for the reporter".
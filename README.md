# AI Investigation Skills: Code Merge Hyderabad hands-on

Turn a bug ticket into a root-cause report with a copilot **agent**, then teach it your team's style with a **skill**.

* Time: about 15 minutes
* Level: beginner. No Jira, Azure, .NET build or admin rights needed.
* you only **read** the code in this repo. Nothing here needs to compile or run.

This is a tiny, safe version of the **ResolveIQ Jira investigation pipeline** that was shown in the session.

## Get ready (please do this before the session)

1. Install **VS code** (latest) and sign in to **GitHub Copilot**
2. Clone this repo and open the folder:
3. Open **copilot chat** (Ctrl+Alt+I), switch to **Agent** mode, open the agent picker and check that you can see **mini-investigator**.

If you can see `mini-investigator`, you are ready. Then open **Hands_ON.md**

## What is inside

```
.github/
  copilot-instructions.md       workspace rules (always on)
  agent/mini-investigator.agent.md the agent: how to investigate a ticket
  skills/                       <- you will add a skill here (empty on purpose)
templates/jira-comment/SKILL.md    starter  skill for you to copy
solutions/jira-comment/SKILL.md    an example answer (look after you try) 
investigation/
  DEMO-101.md, DEMO-102.md         two sample tickets
  attachments_DEMO-101/app.log     logs for ticket 101
  attachments_DEMO-102/app.log     logs for ticket 102
source/order-service/              small sample code the agent reads
docs/                              product rules the agent compares aganist
```

## Ideas to take back to your team

Any repetitive "read evidence, find the cause, write it up" job can use the same pattern:
a trigger, an agent with clear steps, a few skills for your team's style, and guardrails.
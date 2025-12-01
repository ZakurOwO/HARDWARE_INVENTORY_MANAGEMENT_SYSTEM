# Working with the AI Agent

This project is set up to let the AI assistant help make code changes. Below is what to provide and what to expect when asking for changes.

## How to request changes
- Be specific about what you want (feature, bug fix, refactor) and where it lives (files, namespaces, forms, or controls).
- Mention any special constraints (e.g., keep designer wiring intact, avoid NuGet updates, target framework, database connection requirements).
- If you need copy/paste friendly output, say so explicitly.

## What the agent will do
1. **Read repository guidance**: The agent searches for any `AGENTS.md` files and follows their instructions for the affected files.
2. **Locate relevant code**: It uses fast search tools (like `rg`) to find the functions, classes, or controls that match your request.
3. **Edit carefully**: It keeps existing UI designer hooks, avoids changing unrelated code, and follows the project’s conventions when present.
4. **Test when possible**: The agent runs applicable checks (builds, unit tests) when tools are available. If a tool is missing, it will report the limitation.
5. **Summarize changes**: The final reply lists what changed with file references and which tests were run.
6. **Commit and PR message**: Changes are committed on the current branch with a clear message, then a draft PR message is prepared for your review.

## How changes are added
- The agent edits files directly in this repo and commits them so they sync with your branch.
- It avoids broad searches (`ls -R` or `grep -R`) to keep the workflow fast and safe.
- When designer files are involved, it preserves generated regions and only touches the necessary event wiring or code-behind.

## Tips for smoother iterations
- Provide exact control or handler names (e.g., `LoginButton_Click`, `Settings_Signout.btnSignOut_Click`).
- Share expected data flows (e.g., audit should log login/logout with `UserSession` info) and any database schema you rely on.
- If you need a file in full for copy/paste, ask for the entire file in the response.

With these details, the agent can make targeted updates while keeping the repository stable.

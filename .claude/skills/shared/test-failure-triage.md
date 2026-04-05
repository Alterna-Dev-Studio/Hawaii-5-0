# Test Failure Triage Protocol

When a test failure occurs and you suspect it is unrelated to your changes, you MUST follow this protocol before dismissing it. Do NOT claim a failure is "pre-existing" or "flaky" without verification.

## Step 1: Verify on the base branch

Stash your changes and check out the base branch to run the specific failing test(s):

```bash
git stash push -u  # stash including untracked files; note whether a stash was created
git checkout main  # or the appropriate base branch
# Run ONLY the specific failing test(s), not the full suite
dotnet run -- <TestTarget>  # with appropriate filter if possible
```

## Step 2: Evaluate the result

### If the test PASSES on the base branch

Your changes caused the failure. Return to your feature branch, pop the stash, and fix it:

```bash
git checkout <feature-branch>
git stash pop  # only run this if you actually created a stash in Step 1
# Fix the regression
```

### If the test FAILS on the base branch

The failure is genuinely pre-existing. Proceed to Step 3.

## Step 3: Record the pre-existing failure

1. Record the base branch commit hash: `git rev-parse HEAD`
2. Note the test name, test suite, error message, and commit hash where verified broken

## Step 4: Return to your branch and continue

```bash
git checkout <feature-branch>
git stash pop  # only run this if you actually created a stash in Step 1
```

Continue with your current task. Add the following to the PR description:

> **Known pre-existing failure:** <test name> — fails on base branch at <commit-sha>

## Summary

- NEVER dismiss a test failure as pre-existing without running it on the base branch
- NEVER leave a genuinely pre-existing failure undocumented
- ALWAYS reference pre-existing failures in the PR description

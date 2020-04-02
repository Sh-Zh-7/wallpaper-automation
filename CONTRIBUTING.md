Our team encourages community feedback and contributions. Thank you for your interest in
making our project better! There are several ways you can get involved.

## Finding issues you can help with
Looking for something to work on?
Issues marked ``good first issue``
are a good place to start.

You can also check the ``help wanted``tag to find 
other issues to help with. If you're interested in working on a fix, leave a comment to let everyone know and to help
avoid duplicated effort from others.

## Contributions we accept
 Some general guidelines:

* **DO** create one pull request per Issue, and ensure that the Issue is linked in the pull request.
* **DO** include corresponding tests whenever possible.
* **DO** check for additional occurrences of the same problem in other parts of the codebase before submitting your PR.
* **DO** [link the issue](https://github.com/blog/957-introducing-issue-mentions) you are addressing in the 
   pull request.
* **DO** write a good description for your pull request. More detail is better. Describe *why* the change is being 
   made and *why* you have chosen a particular solution. Describe any manual testing you performed to validate your change.
* **DO NOT** submit a PR unless it is linked to an Issue marked 
   `triage approved` 
   This enables us to have a discussion on the idea before anyone invests time in an implementation.
* **DO NOT** merge multiple changes into one PR unless they have the same root cause.
* **DO NOT** submit pure formatting/typo changes to code that has not been modified otherwise.

## Making changes to the code

### Testing
Your change should include tests to verify new functionality wherever possible. Code should be
structured so that it can be unit tested independently of the UI. 
should be used where automated testing is not feasible.

### Git workflow
Our project uses the [GitHub flow](https://guides.github.com/introduction/flow/) where most
development happens directly on the `master` branch. The `master` branch should always be in a
healthy state which is ready for release.

If your change is complex, please clean up the branch history before submitting a pull request.
You can use ``git rebase``
to group your changes into a small number of commits which we can review one at a time.

When completing a pull request, we will generally squash your changes into a single commit. Please
let us know if your pull request needs to be merged as separate commits.

## Review Process
After submitting a pull request, members of the our team will review your code. We will
assign the request to an appropriate reviewer. Any member of the community may
participate in the review, but at least one member of the team will ultimately approve
the request.

Often, multiple iterations will be needed to responding to feedback from reviewers. Try looking at
``past pull request`` to see what the experience might be like.
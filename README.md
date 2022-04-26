## Quick start guide

#### Get token
1. Open [Discord](https://discord.com/channels/@me) in any browser (you can also use the desktop client, but then you first have to do [this](https://www.reddit.com/r/discordapp/comments/sc61n3/comment/hu4fw5x/)).
2. Log in, if you are not already logged in.
3. Press <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>I</kbd> or <kbd>F12</kbd> to open the developer tools.
4. Go to the `Network` tab.
5. In the filter box, type `/api/v9`.
6. Refresh the page/client with <kbd>Ctrl</kbd> + <kbd>R</kbd> or <kbd>F5</kbd>.
7. Click any of the items in the list that just appeared.
8. On the right side, under `Headers`, find an entry called `authorization` (it should be under `Request Headers`).
9. Copy the value, this is your token.

![How to get the token](docs/img/token.png)

#### Use token
1. In a terminal, go to the `Turbulence.CLI` directory.
2. Run `dotnet user-secrets set token [your token here]`.

Now you can run `Turbulence.CLI`.
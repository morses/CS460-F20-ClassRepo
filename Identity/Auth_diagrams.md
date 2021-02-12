Using Markdown Preview Enhanced plug-in for VS Code and <a href="https://bramp.github.io/js-sequence-diagrams/">js-sequence-diagrams</a>
```sequence {theme="simple"}
User Agent -> Origin Server: GET /Login
Origin Server --> User Agent: HTML + RequestVerificationToken
Note left of User Agent: User types in \nemail and password
User Agent -> Origin Server: POST /Login \n(Email,Password,RememberMe,token)
Origin Server -> _signInManager: PasswordSignInAsync\n(Email,Password,RememberMe)
Note right of _signInManager: validate\ncredentials
_signInManager --> Origin Server: result
Origin Server --> User Agent: 302 / \nset-cookie (auth token)
Note left of User Agent: Logged\nin
User Agent -> Origin Server: GET / + \n(🍪 auth token)
Note right of Origin Server: [AllowAnonymous]
Origin Server --> User Agent: 200 HTML
User Agent -> Origin Server: GET /Apples/Eaten + \n(🍪 auth token)
Note right of Origin Server: [Authorize]
Origin Server -> Authentication \nAction Filter: onActionExecuting\n(auth token)
Note right of Authentication \nAction Filter: validate\ntoken
Authentication \nAction Filter --> Origin Server: OK
Note right of Origin Server: query our\ndatabase
Origin Server --> User Agent: 200 {apples: [...]}
Note left of User Agent: Sees apples\neaten
```
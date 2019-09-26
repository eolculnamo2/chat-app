<script>
  // props
  export let goToLogin = () => null;

  // state
  let username;
  let password;
  let confirmPassword;
  let imageUrl;

  // todo add validations
  async function register() {
    const payload = {
      username,
      password,
      imageUrl
    };

    const response = await fetch("/api/userauthentication/register", {
      method: "POST",
      body: JSON.stringify(payload),
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "same-origin"
    });

    if (response) alert("Registration Successful");
    else alert("Registration Failed");
  }
</script>

<style>
  .login-link {
    color: steelblue;
    text-decoration: underline;
    cursor: pointer;
  }
  .login-link:hover {
    opacity: 0.8;
  }
</style>

<div>Username</div>
<input bind:value={username} />
<div>Password</div>
<input bind:value={password} type="password" />
<div>Confirm Password</div>
<input bind:value={confirmPassword} type="password" />
<div>Profile Image URL</div>
<input bind:value={imageUrl} />
<button on:click={register}>Register</button>
<div class="login-link" on:click={goToLogin}>Go to Login</div>

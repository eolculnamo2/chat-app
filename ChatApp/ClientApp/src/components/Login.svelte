<script>
  import * as userStore from "../../store/userInformation.js";
  import { navigate } from "svelte-routing";

  // props
  export let goToRegistration = () => null;

  // state
  let username;
  let password;

  async function login() {
    const response = await fetch("/api/userauthentication/login", {
      method: "POST",
      body: JSON.stringify({ username, password }),
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "same-origin"
    });
    const data = await response.json();
    if (data) {
      if (data.username) userStore.username.set(data.username);
      if (data.imageUrl) userStore.imageUrl.set(data.imageUrl);
      navigate("/chat");
    }
  }
</script>

<style>
  .register-link {
    color: steelblue;
    text-decoration: underline;
    cursor: pointer;
  }
  .register-link:hover {
    opacity: 0.8;
  }
</style>

<div>Username</div>
<input bind:value={username} />
<div>Password</div>
<input bind:value={password} type="password" />
<button on:click={login}>Login</button>
<div class="register-link" on:click={goToRegistration}>
  Register New Account
</div>

<script>
  import Login from "../components/Login.svelte";
  import Register from "../components/Register.svelte";
  let showLogin = true;
  const setShowLogin = value => (showLogin = value);
</script>

{#if showLogin}
  <Login goToRegistration={() => setShowLogin(false)} />
{:else}
  <Register goToLogin={() => setShowLogin(true)} />
{/if}

<template>
  <HeaderComponent header="ADMIN PANEL - GAWEK Tree"></HeaderComponent>
  <div class="container">
    <AdminListComponent> </AdminListComponent>
  </div>
</template>
<script lang="ts">
import AdminListComponent from "../components/Admin/AdminListComponent.vue";
import HeaderComponent from "../components/HeaderComponent.vue";

export default {
  name: "MainView",
  props: {
    msg: String,
  },
  components: {
    AdminListComponent,
    HeaderComponent,
  },
  records: null,
  data() {
    return {
      records: null,
    };
  },

  setup() {
    return {};
  },

  async created() {},
};
</script>
<style scoped>
.container {
  margin: 5%;
}
</style>
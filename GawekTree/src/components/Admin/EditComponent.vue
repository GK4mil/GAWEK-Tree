<template>
  <div>
    <h3>Edit Child</h3>
    <label>Current node: </label>
    <label>{{ NodeObj.name }}</label>
    <hr />
    <label>Current parent of selected node: </label>
    <label>{{ parentObj?.name }}</label>
    <hr />
    <button @click="showParentList">Choose another parent</button>
    <div v-if="showEditParent">
      <hr />
      <NodeChangeListComponent @solveNewParent="solveNewParent" />
    </div>

    <div>
      <input v-model="nameValue" placeholder="Insert name" ref="inputName" />
    </div>
    <div>
      <button @click="tryToEdit">Edit</button>
    </div>
  </div>
  <hr />
</template>

<script lang="ts">
import NodeChangeListComponent from "./NodeChangeListComponent.vue";
import { treeService } from "../../services/TreeService";

export default {
  name: "EditComponent",
  showEditParent: false,
  parentObj: null,
  nameValue: "",
  props: {
    NodeObj: Object,
  },
  watch: {
    NodeObj: async function (newObj) {
      this.parentObj = await treeService.getObjectByGuid(newObj.parentGuid);
      this.nameValue = newObj.name;
    },
  },
  components: {
    NodeChangeListComponent,
  },

  data() {
    return {
      showEditParent: false,
      nameValue: this.NodeObj?.name,
      parentObj: null,
    };
  },
  async mounted() {
    this.parentObj = await treeService.getObjectByGuid(
      this.NodeObj?.parentGuid
    );
  },
  methods: {
    showParentList: function () {
      this.showEditParent = !this.showEditParent;
    },
    tryToEdit: function () {
      if (this.nameValue.length == 0) {
        alert("Empty name field");
        this.$refs.inputName.focus();
        return;
      }
      const requestOptions = {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          parentGuid: this.parentObj?.guid,
          name: this.nameValue,
        }),
      };

      return fetch(
        "https://localhost:5001/api/Tree/child/" + this.NodeObj?.guid,
        requestOptions
      )
        .then(async (response) => {
          const data = await response.json();
          console.log(response);
          if (!response.ok) {
            const error = data.message;
            return Promise.reject(error);
          }
          this.$emit("closeEditFormEvent", this.NodeObj?.parentGuid);
          alert("Child edited");

          return data.guid;
        })
        .catch((error) => {
          alert(error);
        });
    },
    async solveNewParent(id: any) {
      this.parentObj = await treeService.getObjectByGuid(id);
    },
  },
};
</script>
<style >
button {
  margin-bottom: 10px;
}
input {
  margin-bottom: 10px;
}
</style>

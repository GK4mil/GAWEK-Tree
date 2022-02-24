<template>
  <div>
    <h3>Add Child</h3>
    <label>Parent: </label>
    <label>{{ NodeObj.name }}</label>
    <div>
      <input v-model="nameValue" placeholder="Insert name" ref="inputName" />
    </div>
    <div>
      <button @click="tryToAdd">Add</button>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  name: "AddComponent",
  nameValue: "",
  props: {
    NodeObj: Object,
  },
  data() {
    return {
      nameValue: "",
    };
  },
  methods: {
    tryToAdd: function () {
      if (this.nameValue.length == 0) {
        alert("Empty name field");
         (this.$refs['inputName'] as any).focus();
  
        return;
      }

      const requestOptions:any = {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          parentGuid: this.NodeObj?.guid,
          name: this.nameValue,
        }),
      };
      return fetch("https://localhost:5001/api/Tree/child/", requestOptions)
        .then(async (response) => {
          const data = await response.json();
          console.log(response);
          if (!response.ok) {
            const error = data.message;
            return Promise.reject(error);
          }
          this.$emit("closeAddFormEvent",this.NodeObj?.guid);
          alert("Child added");
          return data.guid;
        })
        .catch((error) => {
          alert(error);
        });
    },
  },
};
</script>

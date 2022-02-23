<template>
 
  <div v-if="showEditForm">
     <EditComponent @closeEditFormEvent="handleEditFormClose" :NodeObj="parent"/>
   
    <button @click="handleEditFormClose">
      Close edit form
    </button>
  </div>
  <div v-if="showAddForm">
    <AddComponent @closeAddFormEvent="handleAddFormClose" :NodeObj="parent" />
    <button @click="handleAddFormClose">
      Close add form
    </button>
  </div>
  <button @click="showVariable">Show full tree</button>
  <div>
    <h3>Tree view</h3>
    <AdminChildComponent
      :key="showFullTree"
      :toExpand="showFullTree"
      @solve="solve"
      :node="treeData"
      @solveAdd="solveAdd"
      @solveDelete="solveDelete"
      @solveEdit="solveEdit"
    />
  </div>
</template>

<script lang="ts">
import AdminChildComponent from "../Admin/AdminChildComponent.vue";
import AddComponent from "./AddComponent.vue";
import EditComponent from './EditComponent.vue';
import { treeService } from "../../services/TreeService";

export default {
  name: "AdminListComponent",
  components: {
    AdminChildComponent,
    AddComponent,
    EditComponent,
  },
  treeData: [],
  showAddForm: false,
  showEditForm: false,
  showFullTree: false,
  parent: Object,
  data() {
    return {
      treeData: [],
      showAddForm: false,
      showEditForm: false,
      showFullTree: false,
     
    };
  },
  async created() {
    this.treeData = await treeService.skeleton();
  },
  methods: {
    async handleAddFormClose(id:any) {
      await this.solve(id);
      this.showAddForm = false;
    },
    async handleEditFormClose(id:any) {
      var result = await treeService.getObjectByGuid(id);
      if(result!=null)
        await this.solve(result.parentGuid);
      this.showEditForm = false;
    },
    showVariable() {
      this.showFullTree = !this.showFullTree;
    },
    solve: async function (id: any) {
      var res = treeService.findRecuParent(id, [this.treeData]);

      if (res !== null) {
        res.children = [];
        var children = await treeService.grabChildren(id);

        if (children != null) {
          for (var i in children) {
            if (
              !treeService.checkIfExistsInArray(res.children, children[i].guid)
            )
              res.children.push({
                name: children[i].name,
                guid: children[i].guid,
                parentGuid: children[i].parentGuid,
                children: [],
              });
          }
        }
      }
    },
    solveEdit: async function (id: any) {
      this.parent = await treeService.getObjectByGuid(id);
      this.showEditForm = true;
      this.showAddForm = false;
    },
    solveDelete: async function (id: any) {
      if (confirm("Do you want to remove node and their children?")) {
        const requestOptions = {
          method: "DELETE",
        };
        this.parent = await treeService.getObjectByGuid(id);
        fetch("https://localhost:5001/api/Tree/" + id, requestOptions)
          .then(async (response) => {
            const data = response;

            if (!response.ok) {
              const error = data.message;
              return Promise.reject(error);
            }

            alert("Child deleted");
            await this.solve(this.parent.parentGuid);
            return data.guid;
          })
          .catch((error) => {
            alert(error);
          });
      }
    },
    solveAdd: async function (id: any) {
      this.parent = await treeService.getObjectByGuid(id);
      this.showAddForm = true;
      this.showEditForm = false;
    },
  },
};
</script>
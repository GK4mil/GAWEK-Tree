<template>
  <button @click="showVariable">Show full tree</button>
  <div>
    <h3>Select new parent to edited node</h3>
    <NodeChangeChildComponent
      :key="showFullTree"
      :toExpand="showFullTree"
      @solve="solve"
      :node="treeData"
      @solveNewParent="solveNewParent"
    />
  </div>
</template>

<script lang="ts">
import NodeChangeChildComponent from "../Admin/NodeChangeChildComponent.vue";
import { treeService } from "../../services/TreeService";

export default {
  name: "NodeChangeListComponent",
  components: {
    NodeChangeChildComponent,
  },
  treeData: [],
  showFullTree: false,
  data() {
    return {
      treeData: [],
      showFullTree: false,
    };
  },
  async created() {
    this.treeData = await treeService.skeleton();
  },
  methods: {
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

    solveNewParent(id: any) {
      this.$emit("solveNewParent", id);
    },
  },
};
</script>
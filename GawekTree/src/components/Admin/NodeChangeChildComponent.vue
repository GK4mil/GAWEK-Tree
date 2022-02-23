<template>
  <div>
    <div
      @click="nodeClicked(node.guid)"
      :style="{ margin: '10px', 'margin-left': `${depth * 40}px`, display: 'inline-block' }"
    >
      {{ node.name }}
    </div>

    <div :style="{ display: 'inline-block' }">
      <button
        @click="$emit('solveNewParent', node.guid)"
        class="NewParent"
      >
        Select this node
      </button>
    </div>
    <hr>
    <div v-if="expanded">
      <NodeChangeChildComponent @solveNewParent="solveNewParent"
        :toExpand="toExpand"
        @solve="solveChild"
        v-for="child in node.children"
        :key="child.name"
        :node="child"
        :depth="depth + 1"
      />
    </div>
  </div>
</template>

<script lang="ts">
export default {
  name: "NodeChangeChildComponent",
  props: {
    node: Object,
    toExpand: Boolean,
    depth: {
      type: Number,
      default: 0,
    },
  },
  expanded: false,
  created() {
    if (this.expanded) {
      this.solveChild(this.node?.guid);
    }
  },
  data() {
    return {
      expanded: this.toExpand,
      howmuch: this.depth,
    };
  },
  methods: {
    nodeClicked(id: any) {
      this.expanded = !this.expanded;
      this.solveChild(id);
    },
    solveChild(id: any) {
      this.$emit("solve", id);
    },
    solveNewParent(id:any)
        {
            this.$emit('solveNewParent',id);     
        },
  },
};
</script>
<style scoped>
.NewParent {
  color: rgb(0, 87, 26);
  font-weight: bold;
}
</style>
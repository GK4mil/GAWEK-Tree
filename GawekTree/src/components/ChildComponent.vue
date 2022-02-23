<template>
  <div>
    <div
      @click="nodeClicked(node.guid)"
      :style="{ margin: '10px', 'margin-left': `${depth * 40}px` }"
    >
      {{ node.name }}
    </div>
    <hr />
    <div v-if="expanded">
      <ChildComponent
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
  name: "ChildComponent",
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
  },
};
</script>
<style scoped>
.div2 {
  border: 1px;
  border: solid;
  border-color: darkblue;
}
</style>
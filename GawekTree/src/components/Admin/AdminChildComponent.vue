<template>
  <div>
    <div
      @click="nodeClicked(node.guid)"
      :style="{
        display: 'inline-block',
        margin: '10px',
        'margin-left': `${depth * 40}px`,
      }"
    >
      {{ node.name }}
    </div>
    <div :style="{ display: 'inline-block' }">
      <button
        v-if="node.name != 'root'"
        @click="$emit('solveEdit', node.guid)"
        class="edit"
      >
        Edit
      </button>
      <button
        v-if="node.name != 'root'"
        @click="$emit('solveDelete', node.guid)"
        class="del"
      >
        X
      </button>
      <button @click="$emit('solveAdd', node.guid); expanded=true" class="add">+</button>
    </div>
    <div v-if="expanded">
      <AdminChildComponent
        :toExpand="toExpand"
        @solve="solveChild"
        @solveAdd="solveAdd"
        @solveDelete="solveDelete"
        @solveEdit="solveEdit"
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
  name: "AdminChildComponent",
  expanded: false,
  props: {
    node: Object,
    toExpand: Boolean,
    depth: {
      type: Number,
      default: 0,
    },
  },

  created() {
    if (this.expanded) {
      this.solveChild(this.node?.guid);
    }
  },

  data() {
    return {
      expanded: this.toExpand,
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
    solveEdit(id: any) {
      this.$emit("solveEdit", id);
    },
    solveAdd(id: any) {
      this.$emit("solveAdd", id);
      this.expanded = true;
    },
    solveDelete(id: any) {
      this.$emit("solveDelete", id);
    },
  },
};
</script>
<style scoped>
div {
  margin: 5px;
}
button {
  margin-left: 10px;
}
.add {
  color: rgb(0, 87, 26);
  font-weight: bold;
}
.edit {
  color: rgb(255, 187, 0);
  font-weight: bold;
}
.del {
  color: red;
  font-weight: bold;
}
</style>
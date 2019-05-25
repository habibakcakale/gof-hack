<template>
  <div v-bind="$attrs" class="elevation-2 pa-3 ma-1" v-on="$listeners">
    <chartist
      slot="header"
      :data="data"
      :event-handlers="eventHandlers"
      :options="options"
      :ratio="ratio"
      :responsive-options="responsiveOptions"
      :type="type"
    />
    <slot />
    <slot slot="actions" name="actions" />
  </div>
</template>

<script>
export default {
  name: "TheChartCard",
  inheritAttrs: false,
  props: {
    data: {
      type: Object,
      default: () => ({})
    },
    eventHandlers: {
      type: Array,
      default: () => []
    },
    options: {
      type: Object,
      default: () => ({})
    },
    ratio: {
      type: String,
      default: undefined
    },
    responsiveOptions: {
      type: Array,
      default: () => []
    },
    type: {
      type: String,
      required: true,
      validator: v => ["Bar", "Line", "Pie"].includes(v)
    }
  }
};
</script>

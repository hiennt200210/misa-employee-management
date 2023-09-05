<template>
    <label class="m-label"
        :for="id"
        @mouseover="showTooltip = true"
        @mouseout="showTooltip = false">
        {{ label }}<span v-if="required" class="required-star"> *</span>

        <MTooltip v-if="tooltip" v-show="showTooltip" :content="tooltip" />
    </label>
</template>

<script>
import MTooltip from "../tooltip/MTooltip.vue";

export default {
    name: "MLabel",
    components: {
        MTooltip,
    },
    props: {
        id: String,
        label: String,
        tooltip: String,
        required: Boolean,
    },

    data() {
        return {
            showTooltip: false,
        };
    },
};
</script>

<style scoped>
.m-label {
    position: relative;
    font-family: "Google Sans Medium", sans-serif;
    font-size: 14px;
}

.required-star {
    color: #ff0000;
}
</style>

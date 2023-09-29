<template>
    <tr class="m-table-row">
        <MTableCell v-for="item in headers" :type="item.type" :content="data[`${item.name}`]" />
    </tr>
</template>

<script>
import MTableCell from "@components/bases/tables/MTableCell.vue";

export default {
    name: "MTableRow",

    components: {
        MTableCell,
    },

    props: {
        headers: {
            type: Array,
        },

        data: {
            type: Object,
        },
    },
};
</script>

<style scoped>
@import url(table-row.css);
</style>
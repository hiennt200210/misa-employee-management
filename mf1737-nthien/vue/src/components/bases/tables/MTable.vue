<template>
    <!-- Toolbar -->
    <div class="table-toolbar">
        <div class="toolbar-left">
            <slot name="toolbar-left"></slot>
        </div>

        <div class="toolbar-right">
            <slot name="toolbar-right"></slot>
        </div>
    </div>

    <!-- Table -->
    <div class="table-wrapper">
        <table class="table">
            <thead class="table__thead">
                <slot name="thead"></slot>
            </thead>
            <tbody class="table__tbody">
                <slot name="tbody"></slot>
            </tbody>
        </table>
    </div>

    <!-- Paging -->
    <div class="table-paging">
        <div class="paging-left">
            <slot name="paging-left"></slot>
        </div>
        <div class="paging-right">
            <slot name="paging-right"></slot>
        </div>
    </div>
</template>

<script>
export default {
    name: "MTable",
};
</script>

<style>
@import url(table.css);
</style>

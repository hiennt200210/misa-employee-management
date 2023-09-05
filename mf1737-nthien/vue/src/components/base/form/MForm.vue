<template>
    <div class="m-form">
        <div class="form-title">
            <slot name="title">
                <!-- Tiêu đề Form -->
                <MHeading type="h1" :title="title" />
            </slot>
        </div>

        <!-- Nội dung của Form -->
        <div class="form-content">
            <slot name="content"></slot>
        </div>

        <div class="form-action">
            <slot name="action"></slot>
        </div>
    </div>
</template>

<script>
import MHeading from "../heading/MHeading.vue";

export default {
    name: "MForm",
    components: {
        MHeading,
    },
    props: ["title"],
};
</script>

<style scoped>
@import url(./form.css);
</style>

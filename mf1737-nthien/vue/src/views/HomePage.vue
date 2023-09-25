<template>
    <div>
        <div>Đây là trang chủ</div>
        <mscombobox :value="value" propValue="Prop Value" :propText="propText" :isLoadData="false">
        </mscombobox>
    </div>
</template>

<script>
import mscombobox from 'ms-combobox';

export default {
    name: "HomePage",
    components: {
        mscombobox,
    },
    data() {
        return {
            value: {
                one: "One",
                two: "Two",
            },
            propText: ["One", "Two", "Three"],
        };
    },
};
</script>

<style scoped></style>
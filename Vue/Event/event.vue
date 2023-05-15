<script>
    import confirm from './../Shared/shared.vue';

    export default {
        components: {
            confirm
        },
        data() {
            return {
                event: event,
                newParticipant: {
                    Firstname: null,
                    Lastanme: null,
                    PersonalCode: null,
                    PaymentType: 1,
                    EventId: event.Id
                },
                selected: null
            }
        },
        mounted() {

        },
        methods: {
            addParticipant() {
                let that = this;
                $.ajax({
                    url: "/event/participants/add",
                    type: 'POST',
                    contentType: "application/json",
                    dataType: 'json',
                    data: JSON.stringify(this.newParticipant),
                    success: function (response) {
                        that.newParticipant.Id = response.id;
                        that.event.Participants.push(Object.assign({}, that.newParticipant));

                        that.newParticipant = {
                            Firstname: null,
                            Lastanme: null,
                            PersonalCode: null,
                            PaymentType: 1,
                            EventId: that.event.Id
                        }

                        toastr.success("Osavõtja on edukalt lisatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            confrimRemove: function (participant) {
                this.selected = participant;
                $("#confirm-modal").addClass("shown");
            },
            removeParticipant() {
                let that = this;
                $.ajax({
                    url: "/event/participants/remove",
                    type: 'POST',
                    contentType: "application/json",
                    data: JSON.stringify(this.selected),
                    success: function (response) {
                        that.event.Participants = that.event.Participants.filter(p => p.Id != that.selected.Id);

                        $("#confirm-modal").removeClass("shown");
                        toastr.success("Osavõtja on edukalt kustutatud", "Edu");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            }
        }
    }
</script>

<template>
    <div class="div-centered pl20 pr20">
        <h1 class="form-title">Osavõtjad</h1>
        <div class="form-group">
            <label>Ürituse nimi:</label>
            <span>{{ event.Name }}</span>
        </div>
        <div class="form-group">
            <label>Toimumisaeg:</label>
            <span>{{ event.DateTime }}</span>
        </div>
        <div class="form-group">
            <label>Koht:</label>
            <span>{{ event.Place }}</span>
        </div>
        <div class="form-group flex-ais">
            <label>Osavõtjad:</label>
            <div class="participants-list mt30">
                <ul>
                    <li v-for="participant in event.Participants">
                        <span>{{ participant.Firstname }} {{ participant.Lastname }}</span>
                        <span>{{ participant.PersonalCode }}</span>
                        <a href="#" class="btn">Vaata</a>
                        <button type="button" class="btn" @click="confrimRemove(participant)">Kustuta</button>
                    </li>
                </ul>
            </div>
        </div>
    </div>


    <div class="div-centered">
        <h1 class="form-title">Osavõtjate lisamine</h1>
        <form class="form-main mxw300">
            <div class="form-group">
                <label>Eesnimi:</label>
                <input type="text" v-model="newParticipant.Firstname" />
            </div>
            <div class="form-group">
                <label>Perenimi:</label>
                <input type="text" v-model="newParticipant.Lastname" />
            </div>
            <div class="form-group">
                <label>Isikukood:</label>
                <input type="text" v-model="newParticipant.PersonalCode" />
            </div>
            <div class="form-group">
                <label>Maksmisviis:</label>
                <select v-model="newParticipant.PaymentType">
                    <option value="1">sularaha</option>
                </select>
            </div>
            <div class="form-group flex-jcs">
                <a href="/" class="btn btn-secondary ml0">Tagasi</a>
                <button type="button" class="btn btn-main" @click="addParticipant">Salvesta</button>
            </div>
        </form>
    </div>

    <confirm @confirmed="removeParticipant"></confirm>
</template>